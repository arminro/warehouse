IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WarehouseDb')
BEGIN
  CREATE DATABASE WarehouseDb;
END;