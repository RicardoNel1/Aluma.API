select (users.FirstName + ' ' + users.LastName) ClientName, users.Email ClientEmail, 
--(a.FirstName + ' ' +  a.LastName) Advisor,				--include to query in progress per advisor
applications.Created ApplicationDate,  applications.ApplicationStatus from users
INNER JOIN clients
ON clients.UserId = users.Id
INNER JOIN applications
ON applications.ClientId = clients.Id
--INNER JOIN												--include to query in progress per advisor
--(select FirstName, LastName, advisors.Id from users
--INNER JOIN advisors
--ON users.Id = advisors.UserId
--INNER JOIN applications
--ON advisors.Id = applications.AdvisorId) A
--ON A.Id = applications.AdvisorId
where applications.ApplicationStatus = 1 
AND applications.AdvisorId IS NULL;							--remove to query in progress per advisor


