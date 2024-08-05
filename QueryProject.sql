CREATE DATABASE Test;
USE Test;

CREATE TABLE ms_user(
	user_id BIGINT PRIMARY KEY, 
	user_name VARCHAR(20),
	password VARCHAR(50),
	is_active BIT
);

CREATE TABLE ms_storage_location(
	location_id VARCHAR(10) PRIMARY KEY,
	location_name VARCHAR(100)
);

CREATE TABLE tr_bpkb(
	agreement_number VARCHAR(100) PRIMARY KEY,
	bpkb_no VARCHAR(100),
	branch_id VARCHAR(10),
	bpkb_date DATETIME,
	faktur_no VARCHAR(100),
	faktur_date DATETIME,
	location_id VARCHAR(10) FOREIGN KEY (location_id) REFERENCES ms_storage_location(location_id),
	police_no VARCHAR(20),
	bpkb_date_in DATETIME,
	created_by VARCHAR(20),
	created_on DATETIME,
	last_updated_by VARCHAR(20),
	last_updated_on DATETIME
);

INSERT INTO ms_storage_location(location_id, location_name) VALUES
('0000000001', 'Jakarta Utara'),
('0000000002', 'Jakarta Barat'),
('0000000003', 'Jakarta Selatan'),
('0000000004', 'Jakarta Pusat'),
('0000000005', 'Jakarta Timur');

INSERT INTO ms_user(user_id, user_name, password, is_active) VALUES
(1, 'jhonUmiro', 'admin1*', 1),
(2, 'trisNatan', 'admin2@', 1),
(3, 'hugoRess', 'admin3#', 0);
