--- Display people and their phone numbers
SELECT	DISTINCT p.PersonId, p.FirstName, p.LastName, h.CountryCode, h.AreaCode, h.Number, h.Extension, t.PhoneType
FROM	dbo.Person p
JOIN	dbo.PersonAddress pa ON p.PersonId = pa.PersonId
JOIN	dbo.[Address] a ON pa.AddressId = a.AddressId
JOIN	dbo.PersonPhone pp ON p.PersonId = pp.PersonId
JOIN	dbo.Phone h ON pp.PhoneId = h.PhoneId
JOIN	dbo.PhoneType t ON pp.PhoneTypeId = t.PhoneTypeId
ORDER BY
		p.PersonId
		
--- Display people and their addresses
SELECT	p.PersonId, p.FirstName, p.LastName, a.AddressLine1, a.AddressLine2, a.City, r.RegionName, a.PostalCode, c.CountryName, at.[AddressType] AS 'AddressType'
FROM	dbo.Person p
JOIN	dbo.PersonAddress pa ON p.PersonId = pa.PersonId
JOIN	dbo.[Address] a ON pa.AddressId = a.AddressId
JOIN	dbo.AddressType at ON pa.AddressTypeId = at.AddressTypeId
JOIN	dbo.Region r ON a.RegionId = r.RegionId
JOIN	dbo.Country c ON a.CountryId = c.CountryId
ORDER BY
		p.PersonId

--- Display people and their addresses only if they are in the state of California
SELECT	p.PersonId, p.FirstName, p.LastName, a.AddressLine1, a.AddressLine2, a.City, r.RegionName, a.PostalCode, c.CountryName, at.[AddressType] AS 'AddressType'
FROM	dbo.Person p
JOIN	dbo.PersonAddress pa ON p.PersonId = pa.PersonId
JOIN	dbo.[Address] a ON pa.AddressId = a.AddressId
JOIN	dbo.AddressType at ON pa.AddressTypeId = at.AddressTypeId
JOIN	dbo.Region r ON a.RegionId = r.RegionId
JOIN	dbo.Country c ON a.CountryId = c.CountryId
WHERE	r.RegionName = 'California' AND r.CountryId = 1
ORDER BY
		p.PersonId

--- Show how many people have addresses in each state
SELECT	r.RegionName, COUNT(DISTINCT p.PersonId)
FROM	dbo.Person p
JOIN	dbo.PersonAddress pa ON p.PersonId = pa.PersonId
JOIN	dbo.[Address] a ON pa.AddressId = a.AddressId
JOIN	dbo.Region r ON a.RegionId = r.RegionId
WHERE	r.CountryId = 1		
GROUP BY RegionName

--- Show the % of people that have multiple addresses
SELECT	p.PersonId, p.FirstName, p.LastName		
		, SUM(COUNT(DISTINCT p.PersonId)) over() * 100.0 / (SELECT COUNT(*) FROM Person)
FROM	dbo.Person p
JOIN	dbo.PersonAddress pa ON p.PersonId = pa.PersonId
--JOIN	dbo.[Address] a ON pa.AddressId = a.AddressId
GROUP BY 
		p.PersonId, p.FirstName, p.LastName
HAVING	COUNT(p.PersonId) > 1
-- Example Total 9, 3 without, 2 with 1, 4 > 1 = 44.44
