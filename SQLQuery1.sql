use PW_DB

create table pacjenci (
	id int primary key not null identity(1,1),
	imie varchar(255),
	nazwisko varchar(255)
);

create table zabiegi (
	id int primary key not null identity(1,1),
	nazwa varchar(255),
	cena int
);

create table zlecenia (
	id int primary key not null identity(1,1),
	id_zabiegu int foreign key references zabiegi(id),
	id_pacjenta int foreign key references pacjenci(id)
);

insert into pacjenci (imie, nazwisko) values ('stefan', 'nowak');
insert into pacjenci (imie, nazwisko) values ('albert', 'kowalski');
insert into pacjenci (imie, nazwisko) values ('marita', 'michalkiewicz');
insert into pacjenci (imie, nazwisko) values ('lukasz', 'grzelak');

insert into zabiegi (nazwa, cena) values ('kastracja', 100);
insert into zabiegi (nazwa, cena) values ('czesanie', 32);
insert into zabiegi (nazwa, cena) values ('modelowanie', 98);

set identity_insert dbo.zlecenia on;

insert into zlecenia (id_pacjenta, id_zabiegu) values (9, 5);

select * from pacjenci;

select * from zabiegi;

drop table zlecenia;
drop table pacjenci;
drop table zabiegi;

delete from pacjenci where id=6;

select zl.id, za.nazwa from zlecenia as zl, zabiegi as za where za.id=zl.id_zabiegu and zl.id_pacjenta=1;

select sum(za.cena) from zlecenia as zl, zabiegi as za where za.id=zl.id_zabiegu and zl.id_pacjenta=9;