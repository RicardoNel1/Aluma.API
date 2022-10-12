select (U.FirstName + ' ' + U.LastName) Client, (N.FirstName + ' ' + N.LastName) Advisor, A.Created from applications A
INNER JOIN clients C on C.Id = A.ClientId
INNER JOIN users U on U.Id = C.UserId
INNER JOIN advisors B on B.Id = A.AdvisorId
INNER JOIN users N on N.Id = B.UserId
where N.FirstName <> 'System';
