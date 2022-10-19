select (U.FirstName + ' ' + U.LastName) Client, (B.FirstName + ' ' + B.LastName) Advisor, F.Created from client_fna F
INNER JOIN clients C on F.ClientId = C.Id
INNER JOIN users U on C.UserId = U.Id
INNER JOIN advisors A on A.Id = F.AdvisorId
INNER JOIN users B on B.Id = A.UserId
where B.FirstName <> 'System'
order by F.Created desc;

