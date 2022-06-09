
SELECT * FROM gosmartvalue.Locations;

Update gosmartvalue.Locations l
inner join gosmartvalue.ImportLocations il on l.id = il.locationId
set l.Name = il.Name, l.districtId= il.districtId

INSERT INTO gosmartvalue.Locations (`Id`,`Name`,`DistrictId`)
Select l.locationId,l.Name,l.DistrictId
FROM gosmartvalue.ImportLocations l
Where l.locationId not in (select id from gosmartvalue.Locations )