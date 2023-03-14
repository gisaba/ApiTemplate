select count(1),company from api.employee GROUP by company

select count(1),id_azienda from api.employee GROUP by id_azienda

update api.employee set id_azienda = 1 where company = 'APPLE';

update api.employee set id_azienda = 2 where company = 'EDB';

update api.employee set id_azienda = 3 where company = 'Microsoft';

update api.employee set id_azienda = 4 where company = 'NEW';

drop table api.company;

create TABLE api.company(id bigint,name text);

insert into api.company values(1,'APPLE');
insert into api.company values(2,'EDB');
insert into api.company values(3,'Microsoft');
insert into api.company values(4,'NEW');

select * from api.company ;


select a.* from api.company a join api.employee b ON b.id_azienda = a.id 
where a."name" = 'EDB' limit 100;
