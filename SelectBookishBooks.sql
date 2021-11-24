SELECT * FROM [Book] AS b
LEFT JOIN [BookAuthor] AS ba ON b.Id = ba.BookId
JOIN [Author] AS a ON ba.AuthorId = a.Id;