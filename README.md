# VaccinCenter – Application .NET 8 MVC

Application ASP.NET Core MVC pour la gestion d'un centre de vaccination.

---

## Technologies utilisées
- **Framework** : ASP.NET Core MVC 8.0
- **ORM** : Entity Framework Core 8 (SQLite par défaut, configurable SQL Server)
- **Authentification** : Cookie Authentication (sans ASP.NET Identity)
- **Hash passwords** : BCrypt.Net
- **Patrons de conception** : Repository Pattern (Generic + Specific), Dependency Injection
- **Front-end** : Bootstrap 5.3, Font Awesome 6, Chart.js



---

## Annotations + Fluent API

Les deux approches sont utilisées :
- **Data Annotations** : sur les modèles (`[Required]`, `[StringLength]`, `[Range]`, `[RegularExpression]`, etc.)
- **Fluent API** : dans `ApplicationDbContext.OnModelCreating()` (index uniques, longueurs, relations, conversions)
