SELECT * FROM gosmartvalue.Localities
where id in (SELECT localityId FROM gosmartvalue.ImportLocalities)



UPDATE gosmartvalue.Localities l inner join gosmartvalue.ImportLocalities il on l.id =il.localityId
SET l.Name = il.LocalityName,l.locationId=il.LocationId



SELECT * FROM gosmartvalue.ImportLocalities;