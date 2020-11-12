# DrinkStore
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
