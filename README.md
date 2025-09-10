## Municipal Services Portal – South Africa  
This Project is a Windows Forms application developed in C# using .NET Framework 4.8, aimed at improving municipal service delivery for South African communities. The system offers a streamlined interface for citizens to report issues and interact with local government services efficiently.

---

### Project Capabilities  
The portal allows residents to:  
- Submit detailed reports on municipal issues, including location, category, and optional file attachments  
- Access local events and announcements (feature in development)  
- Track the status of submitted service requests (feature in development)  

---

### Implemented Features  
- **Main Dashboard**: Professionally designed interface with service cards and real-time statistics  
- **Issue Reporting Form**: Validated input fields with support for file uploads  
- **Custom Data Structures**: Includes `CustomList` and `IssueQueue` for optimized data handling  
- **File Upload Support**: Accepts images and documents up to 10MB  
- **Progress Feedback**: Animated progress bar with motivational prompts  
- **Form Validation**: Comprehensive input checks with user-friendly error messages  
- **Responsive UI**: Consistent layout and color scheme across screen sizes  

---

### Upcoming Enhancements  
- Local event listings and announcements  
- Real-time request tracking  
- Advanced search and filtering options  

---

### Technical Architecture  
**Data Structures**  
- `CustomList`: Dynamic array supporting indexing, insertion, and deletion  
- `IssueQueue`: Circular queue for prioritizing issue handling  
- `Issue`: Rich data model with categories, status indicators, and metadata  

**Design Patterns**  
- Singleton: Centralized issue management via `IssueManager`  
- Model-View: Clear separation between UI and data logic  
- Event-Driven: Responsive interface with structured event handling  

**Technology Stack**  
| Component       | Details                          |
|----------------|----------------------------------|
| Framework       | .NET Framework 4.8               |
| Language        | C# 8.0                           |
| UI Technology   | Windows Forms                    |
| IDE             | Visual Studio 2019/2022          |

---

### System Requirements  
- Operating System: Windows 10 or newer  
- Framework: .NET Framework 4.8 (included with Windows 10 May 2019 update and later)  
- Memory: Minimum 512MB RAM  
- Storage: 50MB free space  
- Display: Minimum resolution of 1024×768  

---

### Installation Guide  
**Prerequisites**  
- Visual Studio 2019 or later with .NET Framework workload  
- Git (optional for cloning)

**Setup Methods**  
**Method 1: Visual Studio (Recommended)**  
```bash
git clone https://github.com/Notz05/ST10272691-MunicipalServicesApp-PROG7312.git  
cd MunicipalServiceApp  
```
- Open `MunicipalServiceApp.sln` in Visual Studio  
- Build: `Ctrl + Shift + B`  
- Run: `F5` or click "Start"

**Method 2: MSBuild (Command Line)**  
```bash
msbuild MunicipalServiceApp.sln /p:Configuration=Release  
cd MunicipalServiceApp\bin\Release  
MunicipalServiceApp.exe  
```

**Troubleshooting**  
- Missing Framework: Install .NET Framework 4.8 from Microsoft  
- Build Errors: Clean and rebuild the solution  
- Designer Issues: Delete `bin` and `obj` folders, then reopen the project  

---

### Usage Instructions  
- Launch `MunicipalServiceApp.exe`  
- Navigate the main dashboard  
- Click “Report Issues” to submit a new report  

**Issue Submission Details**  
- Location: Specify address or area  
- Category: Choose from sanitation, roads, utilities, lighting, water, waste, safety, parks, or other  
- Description: Provide detailed context  
- Attachments: Upload optional files (max 10MB; JPG, PNG, PDF, DOC, TXT recommended)  
- Submit: Finalize the report  

---

### Project Structure  
```
MunicipalServiceApp/
├── DataStructures/
│   ├── CustomList.cs
│   └── IssueQueue.cs
├── Models/
│   └── Issue.cs
├── Services/
│   └── IssueManager.cs
├── Forms/
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   ├── ReportIssuesForm.cs
│   └── ReportIssuesForm.Designer.cs
├── Properties/
│   ├── AssemblyInfo.cs
│   ├── Resources.resx
│   └── Settings.settings
├── Program.cs
├── App.config
└── MunicipalServiceApp.csproj
```

---

### Design and Architecture Principles  
**User Interface**  
- Consistent color palette and typography  
- Clear labels and intuitive navigation  
- High contrast and readable fonts  
- Optimized for various screen sizes  

**Code Architecture**  
- Separation of concerns between UI, logic, and data  
- Custom data structures without reliance on built-in generics  
- Comprehensive error handling and user feedback  
- Maintainable and well-documented codebase  

---

### Testing Protocol  
**Manual Testing Checklist**  
- Application launches without errors  
- Main menu displays correctly  
- Issue form opens and validates inputs  
- File uploads function correctly  
- Progress bar and success messages display  
- Statistics update after submission  
- Help button provides accurate information  

**Test Data**  
- Validate with various file types and sizes  
- Test form validation with empty and invalid inputs  
- Navigate between forms to verify transitions  

---

### Security Considerations  
- File size and type validation  
- Input sanitization  
- Graceful error handling  
- Proper disposal of resources  

---

### Future Enhancements  
**Functional Additions**  
- Database integration  
- User authentication  
- Email notifications  
- Advanced filtering and analytics dashboard  
- Mobile app support  
- Multi-language interface  

**Technical Upgrades**  
- RESTful API integration  
- Cloud-based file storage  
- Performance tuning  
- Unit testing coverage  
- Logging and monitoring system  

---

### Support and Documentation  
- Refer to README and inline code comments  
- Ensure system prerequisites are met  
- Review project structure for guidance  

---

### Licensing and Attribution  
Developed as part of a Portfolio of Evidence (PoE) for municipal service improvement in South Africa.  
**Version**: 1.0.0  
**Last Updated**: September 2025  
**Developed By**: Joshua John Pillai
**Target Framework**: .NET Framework 4.8  

---

Let me know if you'd like this adapted into a formal document, presentation, or even a GitHub README layout. I can also help you visualize the architecture or generate diagrams to complement your documentation.
