INSERT INTO Books (Title, Author, Year, Pages)
VALUES
   ('To Kill a Mockingbird', 'Harper Lee', 1960, 336),
   ('1984', 'George Orwell', 1949, 328),
   ('The Great Gatsby', 'F. Scott Fitzgerald', 1925, 180),
   ('The Catcher in the Rye', 'J.D. Salinger', 1951, 224),
   ('Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', 1997, 320),
   ('The Lord of the Rings', 'J.R.R. Tolkien', 1954, 1178),
   ('The Hobbit', 'J.R.R. Tolkien', 1937, 310)

DELETE FROM Books
WHERE Id IN (
    SELECT TOP 7 Id
    FROM Books
    ORDER BY Id DESC
);

SELECT * FROM Books