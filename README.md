# Pelikello

Tool for parents to manage and limit their children's PC gaming time.

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

### Build & Run

```bash
# Clone the repository
git clone <your-fork-or-repo-url>
cd Pelikello

# Restore dependencies and build
dotnet build

# Run the server (from the project root)
dotnet run --configuration Release --project src/Pelikello.App
```

The web dashboard will be available at `http://localhost:5220` (or another port as configured).

---

## Configuration

Configuration is handled via `src/Pelikello.App/appsettings.json`:

```json
{
  "Pelikello": {
    "Db": "Data Source=pelikello.db", // SQLite database file
    "Passcode": "demo1234", // Dashboard login passcode
    "TrackingPollInterval": 10 // Seconds between game activity scans
  }
}
```

- The database file will be created in the `src/Pelikello.App` directory by default.

---

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

## Resources

- [Icons](https://www.untitledui.com/free-icons)

## Author

Jari Helenius

[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- MARKDOWN LINKS & IMAGES -->

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/jari-helenius-a445478a
