USE [ManageStudent]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamSubjects]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamSubjects](
	[IdExam] [int] IDENTITY(1,1) NOT NULL,
	[IdStartTime] [int] NULL,
	[IdEndTime] [int] NULL,
	[IdReSubject] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ExamSubjects] PRIMARY KEY CLUSTERED 
(
	[IdExam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculties]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculties](
	[IdFaculty] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Faculties] PRIMARY KEY CLUSTERED 
(
	[IdFaculty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[IdGrade] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IdTeacher] [int] NOT NULL,
	[idFaculty] [int] NOT NULL,
 CONSTRAINT [PK_Grades] PRIMARY KEY CLUSTERED 
(
	[IdGrade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonTime]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonTime](
	[IdTime] [int] IDENTITY(1,1) NOT NULL,
	[NameTime] [nvarchar](max) NOT NULL,
	[StartTime] [nvarchar](max) NOT NULL,
	[EndTime] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LessonTime] PRIMARY KEY CLUSTERED 
(
	[IdTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReSubjects]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReSubjects](
	[IdReSubject] [int] IDENTITY(1,1) NOT NULL,
	[IdSubject] [int] NOT NULL,
	[IdStudent] [int] NOT NULL,
 CONSTRAINT [PK_ReSubjects] PRIMARY KEY CLUSTERED 
(
	[IdReSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[IdSchedule] [int] IDENTITY(1,1) NOT NULL,
	[Day] [nvarchar](max) NOT NULL,
	[IdTimeStart] [int] NOT NULL,
	[IdTimeEnd] [int] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[IdSchedule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scores]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scores](
	[IdScore] [int] IDENTITY(1,1) NOT NULL,
	[IdReSubject] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[ScoreSubject] [int] NULL,
 CONSTRAINT [PK_Scores] PRIMARY KEY CLUSTERED 
(
	[IdScore] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semesters](
	[IdSemester] [int] IDENTITY(1,1) NOT NULL,
	[Semester] [nvarchar](max) NOT NULL,
	[StartDay] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Semesters] PRIMARY KEY CLUSTERED 
(
	[IdSemester] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SemestersSubject]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemestersSubject](
	[IdSemesterSubject] [int] IDENTITY(1,1) NOT NULL,
	[IdSubjectClass] [int] NOT NULL,
	[IdSchedule] [int] NOT NULL,
 CONSTRAINT [PK_SemestersSubject] PRIMARY KEY CLUSTERED 
(
	[IdSemesterSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[IdStudent] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](5) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Country] [nvarchar](40) NOT NULL,
	[BirthDay] [datetime2](7) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[IdGrade] [int] NOT NULL,
 CONSTRAINT [PK_Students_1] PRIMARY KEY CLUSTERED 
(
	[IdStudent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectClasses]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectClasses](
	[IdClass] [int] IDENTITY(1,1) NOT NULL,
	[NameClass] [nvarchar](max) NOT NULL,
	[NumberClass] [int] NOT NULL,
	[IdSubject] [int] NOT NULL,
	[IdSemester] [int] NOT NULL,
	[SemesterDateStart] [datetime2](7) NOT NULL,
	[SemesterDateEnd] [datetime2](7) NOT NULL,
	[NameTeacher] [nvarchar](max) NULL,
 CONSTRAINT [PK_SubjectClasses] PRIMARY KEY CLUSTERED 
(
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[IdSubject] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IdFaclty] [int] NOT NULL,
	[Fee] [int] NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[IdSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[IdTeacher] [int] IDENTITY(1,1) NOT NULL,
	[NameTeacher] [nvarchar](max) NOT NULL,
	[Salary] [int] NOT NULL,
	[Position] [nvarchar](max) NOT NULL,
	[IdFaculty] [int] NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[IdTeacher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tuitionfees]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tuitionfees](
	[IdTutionFee] [int] IDENTITY(1,1) NOT NULL,
	[IdStudent] [int] NOT NULL,
	[IdSemester] [int] NOT NULL,
	[MoneyTuition] [int] NOT NULL,
	[MoneyPaid] [int] NOT NULL,
 CONSTRAINT [PK_Tuitionfees] PRIMARY KEY CLUSTERED 
(
	[IdTutionFee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/6/2023 4:49:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230809022151_DbInit', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230809080104_Students', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811014930_Students', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811015145_Students', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811031954_Faculties', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811033930_Teachers', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811061057_Grade', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811061340_Grade', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230811085742_Student', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230814031648_Subjects', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230814070350_Subjects', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230816064703_ReSubject', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230816085607_Tuitionfee', N'7.0.9')
GO
SET IDENTITY_INSERT [dbo].[ExamSubjects] ON 

INSERT [dbo].[ExamSubjects] ([IdExam], [IdStartTime], [IdEndTime], [IdReSubject], [Status]) VALUES (78, NULL, NULL, 82, N'chua thi')
INSERT [dbo].[ExamSubjects] ([IdExam], [IdStartTime], [IdEndTime], [IdReSubject], [Status]) VALUES (111, NULL, NULL, 115, N'chua thi')
INSERT [dbo].[ExamSubjects] ([IdExam], [IdStartTime], [IdEndTime], [IdReSubject], [Status]) VALUES (114, NULL, NULL, 118, N'chua thi')
INSERT [dbo].[ExamSubjects] ([IdExam], [IdStartTime], [IdEndTime], [IdReSubject], [Status]) VALUES (118, NULL, NULL, 122, N'chua thi')
INSERT [dbo].[ExamSubjects] ([IdExam], [IdStartTime], [IdEndTime], [IdReSubject], [Status]) VALUES (1115, NULL, NULL, 1119, N'chua thi')
INSERT [dbo].[ExamSubjects] ([IdExam], [IdStartTime], [IdEndTime], [IdReSubject], [Status]) VALUES (1116, NULL, NULL, 1120, N'chua thi')
SET IDENTITY_INSERT [dbo].[ExamSubjects] OFF
GO
SET IDENTITY_INSERT [dbo].[Faculties] ON 

INSERT [dbo].[Faculties] ([IdFaculty], [Name]) VALUES (1, N'Công nghệ thông tin')
INSERT [dbo].[Faculties] ([IdFaculty], [Name]) VALUES (2, N'Công nghệ phần mềm')
INSERT [dbo].[Faculties] ([IdFaculty], [Name]) VALUES (7, N'Kinh tế')
SET IDENTITY_INSERT [dbo].[Faculties] OFF
GO
SET IDENTITY_INSERT [dbo].[Grades] ON 

INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (91, N'61TH-NB', 1, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (99, N'62TH1', 1, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (102, N'KT1', 10, 7)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (103, N'61TH1', 2, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (104, N'62TH-NB', 1, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (105, N'62TH5', 3, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (106, N'62TH3', 3, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (107, N'61TH2', 2, 1)
INSERT [dbo].[Grades] ([IdGrade], [Name], [IdTeacher], [idFaculty]) VALUES (108, N'61PM', 4, 2)
SET IDENTITY_INSERT [dbo].[Grades] OFF
GO
SET IDENTITY_INSERT [dbo].[LessonTime] ON 

INSERT [dbo].[LessonTime] ([IdTime], [NameTime], [StartTime], [EndTime]) VALUES (1, N'Tiết 1', N'6:40', N'7:45')
INSERT [dbo].[LessonTime] ([IdTime], [NameTime], [StartTime], [EndTime]) VALUES (2, N'Tiết 2', N'7:50', N'8:45')
INSERT [dbo].[LessonTime] ([IdTime], [NameTime], [StartTime], [EndTime]) VALUES (3, N'Tiết 3', N'8:50', N'9:45')
INSERT [dbo].[LessonTime] ([IdTime], [NameTime], [StartTime], [EndTime]) VALUES (4, N'Tiết 4', N'9:50', N'10:45')
INSERT [dbo].[LessonTime] ([IdTime], [NameTime], [StartTime], [EndTime]) VALUES (5, N'Tiết 5', N'10:50', N'11:45')
INSERT [dbo].[LessonTime] ([IdTime], [NameTime], [StartTime], [EndTime]) VALUES (6, N'Tiết 6', N'11:50', N'12:45')
SET IDENTITY_INSERT [dbo].[LessonTime] OFF
GO
SET IDENTITY_INSERT [dbo].[ReSubjects] ON 

INSERT [dbo].[ReSubjects] ([IdReSubject], [IdSubject], [IdStudent]) VALUES (82, 23, 321)
INSERT [dbo].[ReSubjects] ([IdReSubject], [IdSubject], [IdStudent]) VALUES (115, 25, 2)
INSERT [dbo].[ReSubjects] ([IdReSubject], [IdSubject], [IdStudent]) VALUES (118, 21, 2)
INSERT [dbo].[ReSubjects] ([IdReSubject], [IdSubject], [IdStudent]) VALUES (122, 46, 2051070)
INSERT [dbo].[ReSubjects] ([IdReSubject], [IdSubject], [IdStudent]) VALUES (1119, 23, 2051061)
INSERT [dbo].[ReSubjects] ([IdReSubject], [IdSubject], [IdStudent]) VALUES (1120, 42, 2051061)
SET IDENTITY_INSERT [dbo].[ReSubjects] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (1, N'Thu 2', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (2, N'Thu 3', 2, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (3, N'Thu 4', 3, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (4, N'Thu 4', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (5, N'Thu 5', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (6, N'Thu 6', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (7, N'Thu 5', 1, 4)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (8, N'Thu 5', 1, 5)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (9, N'Thu 7', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (10, N'Thu 5', 1, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (11, N'thu 6', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (12, N'chu nhat', 1, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (13, N'thu 3', 1, 1)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (14, N'thu 5', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (15, N'chu nhat', 1, 1)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (16, N'thu 2', 1, 1)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (17, N'thu3', 1, 2)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (18, N'thu 4', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (19, N'string', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (20, N'Thu 3', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (21, N'thu 5', 2, 5)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (22, N'Thu 4', 2, 4)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (23, N'Thu 3', 1, 2)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (24, N'Thu 3', 2, 4)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (25, N'thu 5', 2, 4)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (26, N'thu 4', 1, 1)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (27, N'thu 5', 1, 1)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (28, N'thu 4', 2, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (29, N'thu 3', 1, 5)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (30, N'Thứ 2', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (31, N'Thứ 3', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (32, N'Thứ 4', 3, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (33, N'Thứ 4', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (34, N'Thứ 3', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (35, N'Thứ 4', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (36, N'Thứ 5', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (37, N'Thứ 6', 1, 3)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (38, N'Thứ 7', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (39, N'Thứ 6', 4, 6)
INSERT [dbo].[Schedules] ([IdSchedule], [Day], [IdTimeStart], [IdTimeEnd]) VALUES (40, N'Thứ 7', 1, 3)
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[Scores] ON 

INSERT [dbo].[Scores] ([IdScore], [IdReSubject], [Status], [ScoreSubject]) VALUES (77, 82, N'chua thi', NULL)
INSERT [dbo].[Scores] ([IdScore], [IdReSubject], [Status], [ScoreSubject]) VALUES (110, 115, N'chua thi', NULL)
INSERT [dbo].[Scores] ([IdScore], [IdReSubject], [Status], [ScoreSubject]) VALUES (113, 118, N'chua thi', NULL)
INSERT [dbo].[Scores] ([IdScore], [IdReSubject], [Status], [ScoreSubject]) VALUES (117, 122, N'chua thi', NULL)
INSERT [dbo].[Scores] ([IdScore], [IdReSubject], [Status], [ScoreSubject]) VALUES (1114, 1119, N'chua thi', NULL)
INSERT [dbo].[Scores] ([IdScore], [IdReSubject], [Status], [ScoreSubject]) VALUES (1115, 1120, N'chua thi', NULL)
SET IDENTITY_INSERT [dbo].[Scores] OFF
GO
SET IDENTITY_INSERT [dbo].[Semesters] ON 

INSERT [dbo].[Semesters] ([IdSemester], [Semester], [StartDay]) VALUES (1, N'Học kỳ 1 (2023 - 2024)', CAST(N'2023-08-14T07:22:01.4230000' AS DateTime2))
INSERT [dbo].[Semesters] ([IdSemester], [Semester], [StartDay]) VALUES (2, N'Học kỳ 2 (2023 - 2024)', CAST(N'2024-02-09T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Semesters] OFF
GO
SET IDENTITY_INSERT [dbo].[SemestersSubject] ON 

INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (12, 18, 30)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (13, 18, 31)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (17, 21, 31)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (18, 21, 35)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (22, 23, 33)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (25, 25, 35)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (50, 23, 34)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (51, 25, 36)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (52, 41, 30)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (53, 41, 36)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (54, 42, 37)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (55, 42, 38)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (56, 43, 37)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (57, 44, 39)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (58, 45, 40)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (59, 46, 34)
INSERT [dbo].[SemestersSubject] ([IdSemesterSubject], [IdSubjectClass], [IdSchedule]) VALUES (60, 47, 36)
SET IDENTITY_INSERT [dbo].[SemestersSubject] OFF
GO
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051061, N'Nguyễn Thị A', N'Nữ', N'Số 10 Đống Đa', N'Hà Nội', CAST(N'2002-11-06T00:00:00.0000000' AS DateTime2), N'a@gmail.com', N'0123456778', 99)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051062, N'Nguyễn Văn A', N'Nam', N'Số 11 Đống Đa', N'Hà Nam', CAST(N'2002-11-07T05:11:52.1890000' AS DateTime2), N'b@gmail.com', N'0123456778', 99)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051063, N'Lê Văn A', N'Nam', N'Số 12 Đống Đa', N'HCM', CAST(N'2002-12-07T05:11:52.1890000' AS DateTime2), N'c@gmail.com', N'0123456728', 99)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051064, N'Trần Văn A', N'Nam', N'Số 12 Đống Đa', N'HCM', CAST(N'2002-12-07T05:11:52.1890000' AS DateTime2), N'c@gmail.com', N'0123456728', 104)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051065, N'Trần Văn A', N'Nam', N'Số 15 Đống Đa', N'HCM', CAST(N'2002-12-05T05:11:52.1890000' AS DateTime2), N'g@gmail.com', N'0123456728', 104)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051066, N'Trần Thị A', N'Nữ', N'Số 15 Đống Đa', N'Quảng Nam', CAST(N'2002-12-05T05:11:52.1890000' AS DateTime2), N'g@gmail.com', N'0123456732', 104)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051067, N'Trần Thị B', N'Nữ', N'Số 17 Đống Đa', N'Quảng Nam', CAST(N'2002-12-05T05:11:52.1890000' AS DateTime2), N'g@gmail.com', N'0123456732', 105)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051068, N'Trần Thị B', N'Nữ', N'Số 17 Đống Đa', N'Quảng Nam', CAST(N'2002-09-05T05:11:52.1890000' AS DateTime2), N'j@gmail.com', N'0123456732', 102)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051069, N'Trần Thị C', N'Nữ', N'Số 17 Đống Đa', N'Huế', CAST(N'2002-09-05T05:11:52.1890000' AS DateTime2), N'j@gmail.com', N'01234562', 108)
INSERT [dbo].[Students] ([IdStudent], [Name], [Gender], [Address], [Country], [BirthDay], [Email], [Phone], [IdGrade]) VALUES (2051070, N'Nguyễn Văn D', N'Nam', N'Số 5 Tây Sơn', N'Huế', CAST(N'2002-06-12T00:00:00.0000000' AS DateTime2), N'v@gmail.com', N'09876543212', 102)
GO
SET IDENTITY_INSERT [dbo].[SubjectClasses] ON 

INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (18, N'Công nghệ Web lớp 1', 50, 1, 1, CAST(N'2023-08-14T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-20T00:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (21, N'Công nghệ phần mềm lớp 1', 140, 2, 1, CAST(N'2023-09-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-31T00:00:00.0000000' AS DateTime2), N'Nguyễn Thị C')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (23, N'Công nghệ Web lớp 2', 100, 1, 1, CAST(N'2023-08-23T00:00:00.0000000' AS DateTime2), CAST(N'2023-08-31T00:00:00.0000000' AS DateTime2), N'Nguyễn Văn A')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (25, N'Công nghệ Web lớp 3', 60, 1, 1, CAST(N'2023-08-21T00:00:00.0000000' AS DateTime2), CAST(N'2023-08-31T00:00:00.0000000' AS DateTime2), N'Nguyễn Văn A')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (41, N'Công nghệ phần mềm lớp 2', 100, 2, 1, CAST(N'2023-09-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-29T00:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (42, N'Công nghệ phần mềm lớp 3', 60, 2, 1, CAST(N'2023-09-06T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), N'Trần Thị B')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (43, N'Công nghệ Web lớp 1', 30, 1, 2, CAST(N'2024-03-06T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-30T00:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (44, N'Kinh tế vĩ mô lớp 1', 100, 8, 1, CAST(N'2023-09-01T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-08T00:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (45, N'Kinh tế vĩ mô lớp 1', 100, 8, 2, CAST(N'2024-03-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-22T00:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (46, N'Kinh tế vĩ mô lớp 2', 100, 8, 1, CAST(N'2023-09-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), N'')
INSERT [dbo].[SubjectClasses] ([IdClass], [NameClass], [NumberClass], [IdSubject], [IdSemester], [SemesterDateStart], [SemesterDateEnd], [NameTeacher]) VALUES (47, N'Quản lý phần mềm lớp 1', 20, 9, 1, CAST(N'2023-09-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-21T00:00:00.0000000' AS DateTime2), N'')
SET IDENTITY_INSERT [dbo].[SubjectClasses] OFF
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON 

INSERT [dbo].[Subjects] ([IdSubject], [Name], [IdFaclty], [Fee]) VALUES (1, N'Công nghệ Web', 1, 2000000)
INSERT [dbo].[Subjects] ([IdSubject], [Name], [IdFaclty], [Fee]) VALUES (2, N'Công nghệ phần mềm', 1, 3000000)
INSERT [dbo].[Subjects] ([IdSubject], [Name], [IdFaclty], [Fee]) VALUES (8, N'Kinh tế vĩ mô', 7, 2000000)
INSERT [dbo].[Subjects] ([IdSubject], [Name], [IdFaclty], [Fee]) VALUES (9, N'Quản lý phần mềm', 2, 2000000)
SET IDENTITY_INSERT [dbo].[Subjects] OFF
GO
SET IDENTITY_INSERT [dbo].[Teachers] ON 

INSERT [dbo].[Teachers] ([IdTeacher], [NameTeacher], [Salary], [Position], [IdFaculty]) VALUES (1, N'Nguyễn Văn A', 2000000, N'Truong', 1)
INSERT [dbo].[Teachers] ([IdTeacher], [NameTeacher], [Salary], [Position], [IdFaculty]) VALUES (2, N'Nguyễn Văn C', 23334, N'Pho', 1)
INSERT [dbo].[Teachers] ([IdTeacher], [NameTeacher], [Salary], [Position], [IdFaculty]) VALUES (3, N'Đỗ Văn B', 234556, N'Giang vien', 2)
INSERT [dbo].[Teachers] ([IdTeacher], [NameTeacher], [Salary], [Position], [IdFaculty]) VALUES (4, N'Trần Thị A', 22220, N'Giang vien', 1)
INSERT [dbo].[Teachers] ([IdTeacher], [NameTeacher], [Salary], [Position], [IdFaculty]) VALUES (10, N'Lê Thị B', 60000, N'Giang vien', 7)
SET IDENTITY_INSERT [dbo].[Teachers] OFF
GO
SET IDENTITY_INSERT [dbo].[Tuitionfees] ON 

INSERT [dbo].[Tuitionfees] ([IdTutionFee], [IdStudent], [IdSemester], [MoneyTuition], [MoneyPaid]) VALUES (8, 321, 1, 2000000, 0)
INSERT [dbo].[Tuitionfees] ([IdTutionFee], [IdStudent], [IdSemester], [MoneyTuition], [MoneyPaid]) VALUES (12, 2, 1, 7000000, 0)
INSERT [dbo].[Tuitionfees] ([IdTutionFee], [IdStudent], [IdSemester], [MoneyTuition], [MoneyPaid]) VALUES (13, 2051061, 1, 5000000, 0)
INSERT [dbo].[Tuitionfees] ([IdTutionFee], [IdStudent], [IdSemester], [MoneyTuition], [MoneyPaid]) VALUES (14, 2051070, 1, 2000000, 0)
INSERT [dbo].[Tuitionfees] ([IdTutionFee], [IdStudent], [IdSemester], [MoneyTuition], [MoneyPaid]) VALUES (15, 2051069, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Tuitionfees] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Email], [Password]) VALUES (1, N'mm', N'k@gmail.com', N'1')
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password]) VALUES (2, N'2', N'1', N'1')
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password]) VALUES (5, N'34', N'2', N'2')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[ExamSubjects]  WITH CHECK ADD  CONSTRAINT [FK_ExamSubjects_ReSubjects_IdReSubject] FOREIGN KEY([IdReSubject])
REFERENCES [dbo].[ReSubjects] ([IdReSubject])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExamSubjects] CHECK CONSTRAINT [FK_ExamSubjects_ReSubjects_IdReSubject]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Faculties] FOREIGN KEY([idFaculty])
REFERENCES [dbo].[Faculties] ([IdFaculty])
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Faculties]
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD  CONSTRAINT [FK_Grades_Teachers_IdTeacher] FOREIGN KEY([IdTeacher])
REFERENCES [dbo].[Teachers] ([IdTeacher])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Grades] CHECK CONSTRAINT [FK_Grades_Teachers_IdTeacher]
GO
ALTER TABLE [dbo].[ReSubjects]  WITH CHECK ADD  CONSTRAINT [FK_ReSubjects_SubjectClasses_IdSubject] FOREIGN KEY([IdSubject])
REFERENCES [dbo].[SubjectClasses] ([IdClass])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReSubjects] CHECK CONSTRAINT [FK_ReSubjects_SubjectClasses_IdSubject]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_LessonTime_IdTimeEnd] FOREIGN KEY([IdTimeEnd])
REFERENCES [dbo].[LessonTime] ([IdTime])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_LessonTime_IdTimeEnd]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_LessonTime_IdTimeStart] FOREIGN KEY([IdTimeStart])
REFERENCES [dbo].[LessonTime] ([IdTime])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_LessonTime_IdTimeStart]
GO
ALTER TABLE [dbo].[Scores]  WITH CHECK ADD  CONSTRAINT [FK_Scores_ReSubjects_IdReSubject] FOREIGN KEY([IdReSubject])
REFERENCES [dbo].[ReSubjects] ([IdReSubject])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Scores] CHECK CONSTRAINT [FK_Scores_ReSubjects_IdReSubject]
GO
ALTER TABLE [dbo].[SemestersSubject]  WITH CHECK ADD  CONSTRAINT [FK_SemestersSubject_Schedules_IdSchedule] FOREIGN KEY([IdSchedule])
REFERENCES [dbo].[Schedules] ([IdSchedule])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SemestersSubject] CHECK CONSTRAINT [FK_SemestersSubject_Schedules_IdSchedule]
GO
ALTER TABLE [dbo].[SemestersSubject]  WITH CHECK ADD  CONSTRAINT [FK_SemestersSubject_SubjectClasses_IdSubjectClass] FOREIGN KEY([IdSubjectClass])
REFERENCES [dbo].[SubjectClasses] ([IdClass])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SemestersSubject] CHECK CONSTRAINT [FK_SemestersSubject_SubjectClasses_IdSubjectClass]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Grades_IdGrade] FOREIGN KEY([IdGrade])
REFERENCES [dbo].[Grades] ([IdGrade])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Grades_IdGrade]
GO
ALTER TABLE [dbo].[SubjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_SubjectClasses_Semesters_IdSemester] FOREIGN KEY([IdSemester])
REFERENCES [dbo].[Semesters] ([IdSemester])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubjectClasses] CHECK CONSTRAINT [FK_SubjectClasses_Semesters_IdSemester]
GO
ALTER TABLE [dbo].[SubjectClasses]  WITH CHECK ADD  CONSTRAINT [FK_SubjectClasses_Subjects_IdSubject] FOREIGN KEY([IdSubject])
REFERENCES [dbo].[Subjects] ([IdSubject])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubjectClasses] CHECK CONSTRAINT [FK_SubjectClasses_Subjects_IdSubject]
GO
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Faculties_IdFaclty] FOREIGN KEY([IdFaclty])
REFERENCES [dbo].[Faculties] ([IdFaculty])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [FK_Subjects_Faculties_IdFaclty]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Faculties_IdFaculty] FOREIGN KEY([IdFaculty])
REFERENCES [dbo].[Faculties] ([IdFaculty])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Faculties_IdFaculty]
GO
ALTER TABLE [dbo].[Tuitionfees]  WITH CHECK ADD  CONSTRAINT [FK_Tuitionfees_Semesters_IdSemester] FOREIGN KEY([IdSemester])
REFERENCES [dbo].[Semesters] ([IdSemester])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tuitionfees] CHECK CONSTRAINT [FK_Tuitionfees_Semesters_IdSemester]
GO
