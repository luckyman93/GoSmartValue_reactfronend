INSERT INTO `gosmartvalue`.`PropertyDetails` 
(`Id`,`AddedBy`,`AddedOn`,`LastUpdatedBy`,`LastUpdatedOn`,`DataState`,`Metric`,
`DateOfSale`,`SalePrice`,`PlotSize`,`LandUse`,`PropertyType`,`BandClassBandName`,`LocationId`,`LocalityId`,`StreetName`,
`PlotNo`,`IsDeleted`)

SELECT UUID(),'08d780ed-ead5-f12a-1dfa-8ceaf7bfcbb2','2020-03-30 23:23:00','08d780ed-ead5-f12a-1dfa-8ceaf7bfcbb2','2020-03-30 23:23:00',0,0,
`DateOfSale`,CASE `SalePrice` WHEN '' THEN 0 ELSE REPLACE(`SalePrice`,',','') END,`PlotSize`,`LandUseID`,`DevelopTypID`,`BandClassBandName`,ifnull(null,`LocationId`),CASE `LocalityId` WHEN '' THEN null ELSE `LocalityId` END,`StreetName`,
`PlotNumber`,0
FROM gosmartvalue.ImportComparables;





/*
ALTER TABLE `gosmartvalue`.`Comparables` 
DROP FOREIGN KEY `FK_Comparables_Localities_LocalityId`;
ALTER TABLE `gosmartvalue`.`Comparables` 
DROP INDEX `IX_Comparables_LocalityId` ;
;
ALTER TABLE `gosmartvalue`.`Comparables` 
ADD CONSTRAINT `FK_Comparables_Localities_LocalityId`
  FOREIGN KEY ()
  REFERENCES `gosmartvalue`.`Localities` ()
  ON DELETE SET NULL
  ON UPDATE SET NULL;


Executing:
ALTER TABLE `gosmartvalue`.`Comparables` 
DROP FOREIGN KEY `FK_Comparables_Localities_LocalityId`;
ALTER TABLE `gosmartvalue`.`Comparables` 
ADD INDEX `FK_Comparables_Localities_LocalityId_idx` (`LocalityId` ASC),
DROP INDEX `IX_Comparables_LocalityId` ;
;
ALTER TABLE `gosmartvalue`.`Comparables` 
ADD CONSTRAINT `FK_Comparables_Localities_LocalityId`
  FOREIGN KEY (`LocalityId`)
  REFERENCES `gosmartvalue`.`Localities` (`Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

Operation failed: There was an error while applying the SQL script to the database.
ERROR 1091: Can't DROP 'FK_Comparables_Localities_LocalityId'; check that column/key exists
SQL Statement:
ALTER TABLE `gosmartvalue`.`Comparables` 
DROP FOREIGN KEY `FK_Comparables_Localities_LocalityId`
*/