# DrinkStore

***Create model và data***

Bước 1: Tạp thêm Model (dataAnnocatation)

Bước 2: Install emtityframework:
```
     dotnet add <tên prj> package Microsoft.EntityFrameworkCore.Design
     dotnet add <tên prj> package Microsoft.EntityFrameworkCore.SqlServer
     dotnet tool install --global dotnet-ef
```
Bước 3: Add thêm ConectionStrings vào file appsettings.json

Bước 4: Config file Startup.cs

   Sử dụng thêm thư viện Microsoft.EntityFrameworkCore và Model vào Startup.cs để kết nối tới cơ sở dữ liệu

   Sau đó vào iConfiguration thêm "set", 
 
   Add service trong ConfigureServices:
   ```
     services.AddDbContext<StoreDbContext>(opts => {
             opts.UseSqlServer(
                  Configuration["ConnectionStrings:Connection"]);
             });
   ```
Bước 5: Tạo IsRepository

Bước 6: Tạo EfRepository increment từ IStoreRepository

Bước 7: Add Scoped vào files Startup.cs để có thể dùng được IStoreRepository, EfStoreRepository

Bước 8: Tạo Migration: dotnet ef migration add Initial

Bước 9: Tạo dữ liêu ảo trong SeedData.cs

Bước 10: Add SeedData vào Startup.cs

********

***Hiển thị sản phẩm và phân trang***

* Lệnh xóa database
```
  dotnet ef database drop --force --context [StoreDbContext]
```  
  - Nếu không tìm thấy project
```
  dotnet ef database drop --project [Tên project]--force --context StoreDbContext
```
  - Khi cần sửa lại nội dung của database

* chú ý: nếu sửa database xong chạy lại nếu bị lỗi SaveChanges()
   - Xóa database và migrations rồi mới chạy chương trình
	lệnh xóa Migrations: dotnet ef migrations remove

* lỗi: login failed for used , sửa: thêm vào ConnectionStrings: Integrated Security=True;

Bước 1:
	Tạo respository vào Homecontroller để đẩy dữ liệu sang index
	private IStoreRepository repository;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index() => View(repository.Drinks);

Bước 2: sửa bên trong file index.cshtml
	<div>
        <h3>@p.Name</h3>
        @p.Description
        <h4>@p.Price.ToString("c")</h4> //format chuỗi tiền tệ
        </div>
bước 3: phân trang
//thêm vào trong Homecontroller

    public int PageSize = 4; 
    
    public ViewResult Index(int drinkPage = 1)
                => View(repository.Drinks
                    .OrderBy(p => p.DrinkID)
                    .Skip((drinkPage - 1) * PageSize)
                    .Take(PageSize)
                    );


bước 5: tạo file trong model viewmodel > paginginfo

	public int TotalItems { get; set; } tổng số Lượng các phần tử

        public int ItemsPerPage { get; set; } các mục trên mỗi trang

        public int CurrentPage { get; set; } tổng số trang

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);



bước 6: trong DrinkStores tạo một thư mục Infrastructure

   trong Infrastructure tạo file PageLinkTagHelper.cs
```
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.AspNetCore.Mvc.Rendering;
        using Microsoft.AspNetCore.Mvc.Routing;
        using Microsoft.AspNetCore.Mvc.ViewFeatures;
        using Microsoft.AspNetCore.Razor.TagHelpers;
        using DrinkStores.Models.ViewModels;
namespace DrinkStores.Infrastructure
{

    [HtmlTargetElement("div", Attributes="page-model")]

    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

       [ViewContext]
       [HtmlAttributeNotBound]

       public ViewContext ViewContext { get; set; }

       public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            for(int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new
                {
                    drinkPage = i
                });
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
```
Bước 5 : Add using DrinkStores.Models.ViewModels trong file `_ViewImports.cshtml`
```	 .........
	 addTagHelper *, AhihiStore 
```

Bước 7: thêm file ProductsListViewModel.cs 

        //tao view model data
        public IEnumerable<Drink> Drinks { get; set; }
        public PagingInfo PagingInfo { get; set; }


bước 8: trong HomeController sửa 

        public ViewResult Index(int productpage = 1)
            => View(new ProductsListViewModel
            {
                Drinks = repository.Drinks
                .OrderBy(p => p.DrinkID)
                .Skip((productpage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productpage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Drinks.Count()
                }
            });

bước 8: bên trong index.cshtml
```
@model DrinksListViewModel

<div class="box-product">
    @foreach (var p in Model.Drinks)
    {
        <div class="products">
            <h3 class="title">@p.Name</h3>
            <img class="img" src="~/Img/@p.Img" width="300px">
            <div class="text">
                <div class="Description">@p.Description</div>
                <div class="price">@p.Price<span>$</span></div>
            </div>
        </div>
    }
</div>
<div class="page" page-model="@Model.PagingInfo" page-action="Index"></div>
```

************
* Xoá database : PM> dotnet ef database drop --force --context StoreDbContext 
		     dotnet ef database drop --project <Tên project>--force --context StoreDbContext
* Xóa migrations: b1: dotnet ef database drop --project AhihiStore --force --context StoreDbContext
		  b2: dotnet ef migrations remove
		  b3: dotnet ef migrations add Initial
Bước 1 : Add using  AhihiStore.Models (Hiển thị sản phẩm)
	 private IStoreRepository repository;
	 
	 public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
 	 public IActionResult Index() => View(repository.Products); 
	=> HomeControllers.cs 
Bước 2 : Add using System.Linq;(Phân trang)

	 public int PageSize = 4;

	 public IActionResult Index(int productPage = 1)
            => View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize)
            );
	=> HomeControllers.cs 

Bước 3 : Create PagingInfo (folder ViewModels) => folder Models
	//Tag Helper
	public int TotalItems { get; set; } //tổng số mặt hàng
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }  //Trang hiện tại

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage); //tổng số trang

Bước 4 : //Tag Helpers
	 Create folder Infrastructure -> PageLinkTagHelper.cs
	 using Microsoft.AspNetCore.Mvc;
	 using Microsoft.AspNetCore.Mvc.Rendering;
	 using Microsoft.AspNetCore.Mvc.Routing;
	 using Microsoft.AspNetCore.Mvc.ViewFeatures;
	 using Microsoft.AspNetCore.Razor.TagHelpers;
	 using AhihiStore.Models.ViewModels;

	 [HtmlTargetElement("div", Attributes="page-model")]
    	 public class PageLinkTagHelper : TagHelper
    	 {
        	private IUrlHelperFactory urlHelperFactory;

        	public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        	{
            		urlHelperFactory = helperFactory;
        	}

        	[ViewContext]
        	[HtmlAttributeNotBound]
        	public ViewContext ViewContext { get; set; }
	 	public PagingInfo PageModel { get; set; }
        	public string PageAction { get; set; }
        	public override void Process(TagHelperContext context, TagHelperOutput output)
        	{
            		IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			TagBuilder result = new TagBuilder("div");
            		for(int i = 1;i <= PageModel.TotalPages; i++)
            		{
                		TagBuilder tag = new TagBuilder("a");
                		tag.Attributes["href"] = urlHelper.Action(PageAction, new
                		{
                    			productPage = i
                		});
				tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
            		}
			output.Content.AppendHtml(result.InnerHtml);
        	}
    	 }
Bước 5 : Add using AhihiStore.Models.ViewModels => _ViewImports.cshtml(folder View)

	@using DrinkStores
	@using DrinkStores.Models
	@using DrinkStores.Models.ViewModels;
	@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
	@addTagHelper *, DrinkStores
	 

Bước 6 :  Create ProductsListViewModel => folder ViewModels // Tạo View Model Data 

	  public IEnumerable<Product> Products { get; set; }
          public PagingInfo PagingInfo { get; set; }
Bước 7 : Add using AhihiStore.Models.ViewModels; 
	 Edit:
		pprivate IStoreRepository repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }

        //public ViewResult Index(int drinkPage = 1)
        //    => View(repository.Drinks
        //        .OrderBy(p => p.DrinkID)
        //        .Skip((drinkPage - 1) * PageSize)
        //        .Take(PageSize)
        //        );

        public ViewResult Index(int productpage = 1)
            => View(new ProductsListViewModel
            {
                Drinks = repository.Drinks
                .OrderBy(p => p.DrinkID)
                .Skip((productpage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productpage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Drinks.Count()
                }
            });
	 => HomeController.cs  	
Bước 8 : add @model ProductsListViewModel //comment @model IQueryable<Product>	
	 edit @foreach (var p in Model.Products)
	 <div page-model="@Model.PagingInfo" page-action="Index"></div>
	 => Index.cshtml
*Nếu muốn sửa lại route để thay cho route mặc định
vào startup
    
    app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "pagination", "Drinks/Page{drinkPage}",
                    new { Controller = "Home", action = "Index" });
                endpoints.MapDefaultControllerRoute();
            });

bước 9 cài đặt thư viện
xóa thư viện có sẵn

    dotnet tool uninstall --global Microsoft.Web.LibraryManager.Cli

cài thư viện có sẵn
    
    dotnet tool install --global microsoft.web.librarymanager.cli

bước 10: cài đặt bootstrap

        libman init -p cdnjs
        libman install twitter-bootstrap@4.3.1 -d wwwroot/lib/twitter-bootstrap

bước 11: sửa lại file layout thêm bootstrap
bước 12: sửa lại file index thêm bootstrap

Bước 13:
thêm đoạn này vào trong PageLinkTagHelper.cs

	public bool PageClassesEnabled { get; set; } = false;

        public string PageClass { get; set; }

        public string PageClassNormal { get; set; }

        public string PageClassSelected { get; set; }


đoạn code này là để hiển thị màu cho nút phân trang trong index

Bước 14 sử dụng razor component để hiển thị
trong view -> shared tạo file view
copy đoạn code trong vòng lặp for của file index vào file ProductsSummary

Bước 15: sửa file index
        
        @foreach (var p in Model.Drinks)
            {
                <partial name="ProductsSummary" model="p"/>
                //p được truyền sang ProductsSummary bằng model
            }
 





