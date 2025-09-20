<Query Kind="Statements">
  <Connection>
    <ID>69fe10f5-4cbd-43f8-8c7f-c14c85d5f540</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>StarTED2025</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

// Take Home Assginment 1
// Brooke Smith
// Sept 19, 2025


// Question 1
ClubActivities
	.Where(ca => ca.StartDate.Value >= new DateTime(2025, 1, 1)
		&& ca.CampusVenue.Location.ToLower() != "scheduled room"
		&& ca.Name.ToLower() != "btech club meeting")
	.Select(ca => new 
	{
		StartDate = ca.StartDate,
		VenueName = ca.CampusVenue.Location,
		HostingClubName = ca.Club.ClubName,
		ActivityTitle = ca.Name
	})
	.OrderBy(ca => ca.StartDate)
	.Dump();

// Question 2 
Programs
	.Where(p => p.ProgramCourses.Count(pc => pc.Required) >= 22)
	.Select(p => new 
	{
		School = p.SchoolCode.ToUpper() == "SAMIT" ? "School of Advance Media and IT" : p.SchoolCode.ToUpper() == "SEET" ?
			"School of Electrical Engineering Technology" : "Unknown",
		Program = p.ProgramName,
		RequiredCourseCount = p.ProgramCourses.Count(pc => pc.Required),
		OptionalCourseCount = p.ProgramCourses.Count(pc => !pc.Required)
	})
	.OrderBy(p => p.Program)
	.Dump();

// Question 3 
Students
	.Where(s => s.Countries.CountryName.ToUpper() != "CANADA" 
		&& s.StudentPayments.Count() == 0)
	.OrderBy(s => s.LastName)
	.Select(s => new 
	{
		StudentNumber = s.StudentNumber,
		CountryName = s.Countries.CountryName,
		FullName = s.FirstName + " " + s.LastName,
		ClubMembershipCount = s.ClubMembers.Count() == 0 ? "None" : s.ClubMembers.Count().ToString()
	})
	.Dump();

// Question 4 
Employees
	.Where(e => e.Position.Description.ToUpper() == "INSTRUCTOR"
		&& e.ReleaseDate == null 
		&& e.ClassOfferings.Count() > 0)
	.OrderByDescending(e => e.ClassOfferings.Count())
	.ThenBy(e => e.LastName)
	.Select(e => new 
	{
		ProgramName = e.Program.ProgramName,
		FullName = e.FirstName + " " + e.LastName,
		WorkLoad = e.ClassOfferings.Count() > 24 ? "High" : e.ClassOfferings.Count() > 8 ? "Med" : "Low"
	})
	.Dump();

// Question 5 
Clubs
	.Select(c => new
	{
		Supervisor = c.Employee == null ? "Unknown" : c.Employee.FirstName + " " + c.Employee.LastName,
		Club = c.ClubName, 
		MemberCount = c.ClubMembers.Count(),
		Activities = c.ClubActivities.Count() == 0 ? "None Schedule" : c.ClubActivities.Count().ToString()
	})
	.OrderByDescending(c => c.MemberCount)
	.Dump();
