select (users.FirstName + users.LastName) Client, users.Email, users.Created from clients
INNER JOIN users on clients.UserId = users.Id
order by users.Id desc;