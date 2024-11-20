CREATE DATABASE TournamentConfiguration;
GO
USE TournamentConfiguration;

-- Table creation
CREATE TABLE tb_Tournament (id INTEGER IDENTITY(1001,1) NOT NULL PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            description VARCHAR(255) NOT NULL);
INSERT INTO tb_Tournament(name, description)
  VALUES 
  ('Tournament1', 'Tournamen 1 description'),
  ('Tournament2', 'Tournamen 1 description');

  select * from tb_Tournament;


USE TournamentConfiguration;
EXEC sys.sp_cdc_enable_db;
ALTER DATABASE TournamentConfiguration SET CHANGE_TRACKING = ON (CHANGE_RETENTION = 2 DAYS, 
                         AUTO_CLEANUP = ON);


-- Enable CDC at Table level
EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'tb_Tournament', @role_name = NULL,
@capture_instance='tb_Tournament_instance', @supports_net_changes = 1;

-- source_schema is the database object
-- source_name is the table name
-- capture_instance is the name of the instance of the CDC enabled table


INSERT INTO tb_Tournament(name, description)
  VALUES 
  ('Tournament4', 'Tournamen 4 description');