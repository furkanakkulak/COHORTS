--test veritabanınızda employee isimli sütun bilgileri id(INTEGER), name VARCHAR(50), birthday DATE, email VARCHAR(100) olan bir tablo oluşturalım-
--CREATE TABLE employee(id INTEGER, name VARCHAR(50), birthday DATE, email VARCHAR(100));

--Oluşturduğumuz employee tablosuna 'Mockaroo' servisini kullanarak 50 adet veri ekleyelim-
insert into employee (name, birthday, email) values ('Carree', '1992-09-12', 'cshurman0@sun.com');
insert into employee (name, birthday, email) values ('Bern', '1995-09-08', 'bseater1@oakley.com');
insert into employee (name, birthday, email) values ('Frederik', '1994-05-03', 'fcareless2@blogger.com');
insert into employee (name, birthday, email) values ('Meryl', '1994-10-08', 'mocuolahan3@infoseek.co.jp');
insert into employee (name, birthday, email) values ('Sven', '2001-08-24', 'srodda4@ucoz.ru');
insert into employee (name, birthday, email) values ('Benji', '2003-02-08', 'ballwright5@ihg.com');
insert into employee (name, birthday, email) values ('Starr', '1982-01-02', 'srumford6@elpais.com');
insert into employee (name, birthday, email) values ('Heindrick', '1996-07-06', 'hmathiasen7@diigo.com');
insert into employee (name, birthday, email) values ('Lorrie', '1991-01-27', 'lwehnerr8@epa.gov');
insert into employee (name, birthday, email) values ('Neils', '1997-03-21', 'nsowrey9@tinyurl.com');
insert into employee (name, birthday, email) values ('Michell', '1994-02-20', 'mjerreda@i2i.jp');
insert into employee (name, birthday, email) values ('Morley', '1983-12-09', 'mextenceb@topsy.com');
insert into employee (name, birthday, email) values ('Tedda', '1990-02-25', 'tgiblinc@google.co.jp');
insert into employee (name, birthday, email) values ('Web', '1989-06-21', 'wcousind@free.fr');
insert into employee (name, birthday, email) values ('Devy', '2000-04-12', 'dllelwelne@constantcontact.com');
insert into employee (name, birthday, email) values ('Darci', '1983-01-27', 'dlebournf@smh.com.au');
insert into employee (name, birthday, email) values ('Adolpho', '2000-01-17', 'alutherg@theguardian.com');
insert into employee (name, birthday, email) values ('Jo', '1988-12-03', 'jtreadwayh@thetimes.co.uk');
insert into employee (name, birthday, email) values ('Aveline', '1990-06-18', 'astapforthi@imageshack.us');
insert into employee (name, birthday, email) values ('Evaleen', '1991-12-08', 'etosdevinj@google.cn');
insert into employee (name, birthday, email) values ('Iago', '2000-07-30', 'ibertomeuk@domainmarket.com');
insert into employee (name, birthday, email) values ('Wit', '1996-05-26', 'wosullivanl@wiley.com');
insert into employee (name, birthday, email) values ('Tad', '1989-06-05', 'tnowerm@squarespace.com');
insert into employee (name, birthday, email) values ('Coop', '2002-12-23', 'cchazerandn@alibaba.com');
insert into employee (name, birthday, email) values ('Bordie', '2000-09-02', 'blesurfo@ucla.edu');
insert into employee (name, birthday, email) values ('Brittani', '1992-03-30', 'bbravingtonp@cafepress.com');
insert into employee (name, birthday, email) values ('Obediah', '1996-01-02', 'oparramoreq@diigo.com');
insert into employee (name, birthday, email) values ('Cindee', '1991-09-20', 'csharplessr@g.co');
insert into employee (name, birthday, email) values ('Vernice', '2000-03-13', 'vskoates@over-blog.com');
insert into employee (name, birthday, email) values ('Toby', '1999-09-26', 'taront@blogtalkradio.com');
insert into employee (name, birthday, email) values ('Suki', '1996-05-22', 'sslaffordu@typepad.com');
insert into employee (name, birthday, email) values ('Cathe', '1994-12-15', 'cnormavillv@example.com');
insert into employee (name, birthday, email) values ('Ardeen', '1996-08-11', 'atettersellw@craigslist.org');
insert into employee (name, birthday, email) values ('Babb', '2002-04-04', 'bsellarsx@fotki.com');
insert into employee (name, birthday, email) values ('Wilow', '1991-06-10', 'wguilfordy@elpais.com');
insert into employee (name, birthday, email) values ('Abeu', '2001-02-02', 'abennedickz@lulu.com');
insert into employee (name, birthday, email) values ('Marketa', '1992-06-07', 'mcrowest10@xing.com');
insert into employee (name, birthday, email) values ('Lawton', '2003-07-27', 'lcawston11@blinklist.com');
insert into employee (name, birthday, email) values ('Renault', '1986-07-17', 'rhellwig12@google.nl');
insert into employee (name, birthday, email) values ('Kimball', '1994-09-29', 'kalesio13@nsw.gov.au');
insert into employee (name, birthday, email) values ('Lindi', '1981-05-15', 'lraynor14@bbc.co.uk');
insert into employee (name, birthday, email) values ('Jedediah', '1995-03-15', 'jblakeley15@netvibes.com');
insert into employee (name, birthday, email) values ('Patty', '1983-01-18', 'pmicklewicz16@howstuffworks.com');
insert into employee (name, birthday, email) values ('Luciano', '1986-07-19', 'lfilewood17@example.com');
insert into employee (name, birthday, email) values ('Marja', '1995-10-09', 'mpimer18@fastcompany.com');
insert into employee (name, birthday, email) values ('Amabel', '1992-05-26', 'aplunkett19@ft.com');
insert into employee (name, birthday, email) values ('Reinald', '1998-06-23', 'rberget1a@sitemeter.com');
insert into employee (name, birthday, email) values ('Cati', '1995-01-20', 'cleivesley1b@rediff.com');
insert into employee (name, birthday, email) values ('Ursola', '2000-01-17', 'umullen1c@eepurl.com');
insert into employee (name, birthday, email) values ('Debbie', '2002-07-29', 'dsirey1d@hp.com');

--Sütunların her birine göre diğer sütunları güncelleyecek 5 adet UPDATE işlemi yapalım-
UPDATE employee 
SET name = 'Furkan',
    birthday = '2000-07-06',
    email = 'akkulak-furkan26@gmail-com'
WHERE id =1

UPDATE employee
SET name = 'Burak',
    birthday = '1999-08-25',
    email = 'burakcetinkaya2699@gmail-com'
WHERE id = 2

UPDATE employee
SET name = 'Mustafa',
    birthday = '2000-05-07',
    email = 'mustafakarakas@gmail-com'
WHERE id = 3

UPDATE employee
SET name = '',
    birthday = '2002-06-15',
    email = 'examplemail@gmail-com'
WHERE id = 4

UPDATE employee
SET name = 'Şeyma',
    birthday = '1999-11-07',
    email = 'seyma26@gmail-com'
WHERE id = 5

--Sütunların her birine göre ilgili satırı silecek 5 adet DELETE işlemi yapalım-

DELETE FROM employee
WHERE id in(50,45,30,35,25)