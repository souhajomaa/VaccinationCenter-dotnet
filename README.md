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

## Structure du projet

```
VaccinationCenter/
├── Controllers/
│   ├── AccountController.cs     ← Login / Register / Logout
│   ├── HomeController.cs        ← Espace utilisateur (liste, recherche, réservation)
│   ├── AdminController.cs       ← CRUD toutes tables (rôle Admin)
│   └── VaccinApiController.cs   ← Microservices REST API
├── Models/
│   ├── Citoyen.cs
│   ├── Addresse.cs
│   ├── Vaccin.cs
│   ├── CentreVaccination.cs
│   ├── RendezVous.cs
│   ├── Compte.cs
│   └── TypeVaccin.cs (enum: PFizer, Moderna, Jhonson)
├── Data/
│   └── ApplicationDbContext.cs  ← DbContext + Fluent API + Seed
├── Services/
│   ├── IGenericRepository.cs / GenericRepository.cs
│   ├── IVaccinService.cs / VaccinService.cs
│   ├── ICitoyenService.cs / CitoyenService.cs
│   ├── IRendezVousService.cs / RendezVousService.cs
│   ├── ICompteService.cs / CompteService.cs
│   └── IStatistiquesService.cs / StatistiquesService.cs
├── ViewModels/
│   └── ViewModels.cs
├── Views/
│   ├── Account/ (Login, Register, AccessDenied)
│   ├── Admin/   (Dashboard, Vaccins, Citoyens, Centres, RendezVous, Comptes + CRUD)
│   ├── Home/    (Index, Search, Reserver)
│   └── Shared/  (_Layout.cshtml)
└── Program.cs
```

---

## Lancer l'application

### Prérequis
- .NET 8 SDK installé

### Étapes

```bash
# 1. Restaurer les packages
dotnet restore

# 2. Appliquer les migrations (crée vaccination.db automatiquement)
dotnet run
# La base est créée automatiquement au premier lancement via EnsureCreated()
```

Ouvrir : **https://localhost:5001** ou **http://localhost:5000**

---

## Compte administrateur (par défaut)

| Login | Mot de passe |
|-------|-------------|
| admin | Admin@123   |

---

## Fonctionnalités

### Utilisateur (rôle User)
- ✅ Inscription / Connexion
- ✅ Consulter la liste des vaccins disponibles
- ✅ Recherche (par date, type, fournisseur)
- ✅ Réserver un vaccin (rendez-vous)

### Administrateur (rôle Admin)
- ✅ CRUD Vaccins
- ✅ CRUD Citoyens
- ✅ CRUD Centres de vaccination
- ✅ Consultation et suppression des rendez-vous
- ✅ Vue des comptes utilisateurs
- ✅ Dashboard statistiques (graphiques Chart.js)

### API REST (Microservices)
- `GET  /api/vaccinapi`            — Liste des vaccins disponibles
- `GET  /api/vaccinapi/{id}`       — Détail d'un vaccin
- `GET  /api/vaccinapi/search`     — Recherche (type, fournisseur)
- `POST /api/vaccinapi`            — Créer un vaccin (Admin)
- `PUT  /api/vaccinapi/{id}`       — Modifier un vaccin (Admin)
- `DELETE /api/vaccinapi/{id}`     — Supprimer un vaccin (Admin)

---

## Patrons de conception

| Patron | Où |
|--------|----|
| **Repository Pattern** | `IGenericRepository<T>` + implémentations spécifiques |
| **Dependency Injection** | `Program.cs` — `AddScoped<>` |
| **MVC** | Séparation Controller / Model / View |

---

## Annotations + Fluent API

Les deux approches sont utilisées :
- **Data Annotations** : sur les modèles (`[Required]`, `[StringLength]`, `[Range]`, `[RegularExpression]`, etc.)
- **Fluent API** : dans `ApplicationDbContext.OnModelCreating()` (index uniques, longueurs, relations, conversions)
