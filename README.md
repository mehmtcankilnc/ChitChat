## Projeyi Çalıştırma (Local)

### Önkoşullar
- .NET SDK 8.0+
- Git

### Adımlar
```bash
# 1) Depoyu klonla
git clone https://github.com/mehmtcankilnc/ChitChat.git
cd ChitChat

# 2) Bağımlılıkları indir
dotnet restore ChitChat.sln

# 3) Derle
dotnet build ChitChat.sln

# 4) API’yi çalıştır
# Not: URL’yi dışarıya açmak için 0.0.0.0 kullanıyoruz. Gerekirse portu değiştir.
dotnet run --project ./ChitChat.API/ChitChat.API.csproj --urls http://0.0.0.0:8080

