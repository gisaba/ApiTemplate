DROP TABLE api.employee;

CREATE TABLE api.employee (
    id uuid NOT NULL,
	seq_id int8 NOT NULL,
    name varchar(120) NOT NULL,
    salary int8 NOT NULL,
	company varchar(120),
    CONSTRAINT emp_pk PRIMARY KEY (id)
);


WITH salary_list AS (
    SELECT '{1000, 2000, 5000}'::INT[] salary
)
INSERT INTO api.employee
(id, seq_id, name, salary,company)
SELECT gen_random_uuid (), n, 'Employee' || n as name, salary[1 + mod(n, array_length(salary, 1))],
'NEW'
FROM salary_list, generate_series(1, 1000000) as n
 
SELECT * from api.employee LIMIT 1000;

WITH companies_list AS (
    SELECT '{"APPLE", "Microsoft", "EDB"}'::text[] company
)

update api.employee set company = 'APPLE' where seq_id BETWEEN 1 and 3000

update api.employee set company = 'Microsoft' where seq_id BETWEEN 3001 and 100000

update api.employee set company = 'EDB' where seq_id BETWEEN 100001 and 100500

SELECT count(1),company from api.employee group by company;




