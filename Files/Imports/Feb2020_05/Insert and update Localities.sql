SELECT 
*
 FROM gosmartvalue.Localities order by id asc;


SELECT * FROM gosmartvalue.Locations;

SELECT l.Id 'locationId', l.Name 'LocationName', IFNULL(w.Id,"") 'LocationId', IFNULL(w.Name,"") 'LocalityName'
FROM gosmartvalue.Locations l
LEFT JOIN gosmartvalue.Localities w on w.LocationId = l.Id

INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (52,'South East District');

INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (33,'Tapologo');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (33,'Ext 5');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 2');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 3');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 4');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 5');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 6');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 7');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 8');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 9');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Block 10');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Donga North');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Dumelang Industrial');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Francistown Cental');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Gerald Block 5 North');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Gerald Block 5 South');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Gerald Block 6 North');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Gerald Block 6 South');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Gerald Industrial North');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Monarch North');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Monarch South');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Ntshe River Plots');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Satelite');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Selepa & Tati River Plots');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Somrest');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (27,'Tati River Plots North');

INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'BCL Plots');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Botshabelo');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Distance');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Orlando');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Tshabantsha');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'New Stance');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Phase 1');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Phase 2');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Phase 3');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Phase 4');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'Kasane Botshabelo');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'China Town');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'New Town');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'Plateau');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'Kgaphamadi');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'River Front');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'Pimville');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	51	,'Town Centre Selibe Phikwe');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	27	,'Garden View');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	52	,'Kgaphamadi Industrial');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 1');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 2');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 3');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 4');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 5');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 6');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 7');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 8');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 9');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 10');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Unit 11');
INSERT INTO `gosmartvalue`.`Localities` (`LocationId`, `Name`) VALUES (	38	,'Industrial Zone');



UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 2', `LocationId`=27	  WHERE (`Id` =	1	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 3', `LocationId`=27	  WHERE (`Id` =	2	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 4', `LocationId`=27	  WHERE (`Id` =	3	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 5', `LocationId`=27	  WHERE (`Id` =	4	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 6', `LocationId`=27	  WHERE (`Id` =	5	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 7', `LocationId`=27	  WHERE (`Id` =	6	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 8', `LocationId`=27	  WHERE (`Id` =	7	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 9', `LocationId`=27	  WHERE (`Id` =	8	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 10', `LocationId`=27	  WHERE (`Id` =	9	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Donga North', `LocationId`=27	  WHERE (`Id` =	10	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Dumelang Industrial', `LocationId`=27	  WHERE (`Id` =	11	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Francistown Cental', `LocationId`=27	  WHERE (`Id` =	12	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gerald Block 5 North', `LocationId`=27	  WHERE (`Id` =	13	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gerald Block 5 South', `LocationId`=27	  WHERE (`Id` =	14	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gerald Block 6 North', `LocationId`=27	  WHERE (`Id` =	15	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gerald Block 6 South', `LocationId`=27	  WHERE (`Id` =	16	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gerald Industrial North', `LocationId`=27	  WHERE (`Id` =	17	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Monarch North', `LocationId`=27	  WHERE (`Id` =	18	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Monarch South', `LocationId`=27	  WHERE (`Id` =	19	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Ntshe River Plots', `LocationId`=27	  WHERE (`Id` =	20	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Satelite', `LocationId`=27	  WHERE (`Id` =	21	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Selepa & Tati River Plots', `LocationId`=27	  WHERE (`Id` =	22	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Somrest', `LocationId`=27	  WHERE (`Id` =	23	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tati River Plots North', `LocationId`=27	  WHERE (`Id` =	24	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Garden View', `LocationId`=27	  WHERE (`Id` =	25	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'BBS Mall', `LocationId`=33	  WHERE (`Id` =	26	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'block 3', `LocationId`=33	  WHERE (`Id` =	27	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Bontleng', `LocationId`=33	  WHERE (`Id` =	28	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 5', `LocationId`=33	  WHERE (`Id` =	29	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 6', `LocationId`=33	  WHERE (`Id` =	30	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'block 7', `LocationId`=33	  WHERE (`Id` =	31	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'block 8', `LocationId`=33	  WHERE (`Id` =	32	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 9', `LocationId`=33	  WHERE (`Id` =	33	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 10', `LocationId`=33	  WHERE (`Id` =	34	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Broadhurst', `LocationId`=33	  WHERE (`Id` =	35	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Broadhurst (Ginger)', `LocationId`=33	  WHERE (`Id` =	36	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Commerce park', `LocationId`=33	  WHERE (`Id` =	37	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 11', `LocationId`=33	  WHERE (`Id` =	38	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 12', `LocationId`=33	  WHERE (`Id` =	39	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 14', `LocationId`=33	  WHERE (`Id` =	40	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 19', `LocationId`=33	  WHERE (`Id` =	41	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 16', `LocationId`=33	  WHERE (`Id` =	42	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 25', `LocationId`=33	  WHERE (`Id` =	43	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 9', `LocationId`=33	  WHERE (`Id` =	44	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gaborone west', `LocationId`=33	  WHERE (`Id` =	45	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 4', `LocationId`=33	  WHERE (`Id` =	46	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Kgale Hill Town', `LocationId`=33	  WHERE (`Id` =	47	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Old Naledi', `LocationId`=33	  WHERE (`Id` =	48	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phakalane Golf Estate', `LocationId`=33	  WHERE (`Id` =	49	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phakalane ', `LocationId`=33	  WHERE (`Id` =	50	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phase 2', `LocationId`=33	  WHERE (`Id` =	51	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phase 4', `LocationId`=33	  WHERE (`Id` =	52	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tsholofelo West', `LocationId`=33	  WHERE (`Id` =	53	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tsholofelo East', `LocationId`=33	  WHERE (`Id` =	54	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tsholofelo', `LocationId`=33	  WHERE (`Id` =	55	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Taung', `LocationId`=33	  WHERE (`Id` =	56	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tawana', `LocationId`=33	  WHERE (`Id` =	57	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'whitecity', `LocationId`=33	  WHERE (`Id` =	58	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gaborone West Phase 1', `LocationId`=33	  WHERE (`Id` =	59	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Maruapula', `LocationId`=33	  WHERE (`Id` =	60	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Partial', `LocationId`=33	  WHERE (`Id` =	61	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 10', `LocationId`=33	  WHERE (`Id` =	62	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Village', `LocationId`=33	  WHERE (`Id` =	63	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extension 27', `LocationId`=33	  WHERE (`Id` =	64	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tawana', `LocationId`=33	  WHERE (`Id` =	65	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Ledumang', `LocationId`=33	  WHERE (`Id` =	66	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Block 3 (Industrial)', `LocationId`=33	  WHERE (`Id` =	67	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gaborone North', `LocationId`=33	  WHERE (`Id` =	68	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extesion43', `LocationId`=33	  WHERE (`Id` =	69	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tshimotlharo', `LocationId`=33	  WHERE (`Id` =	70	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extesion20', `LocationId`=33	  WHERE (`Id` =	71	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'New Naledi', `LocationId`=33	  WHERE (`Id` =	72	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Gaborone West Industrial', `LocationId`=33	  WHERE (`Id` =	73	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Broadhurst Industrial', `LocationId`=33	  WHERE (`Id` =	74	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Finance Park', `LocationId`=33	  WHERE (`Id` =	75	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Fairgrounds', `LocationId`=33	  WHERE (`Id` =	76	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extesion44', `LocationId`=33	  WHERE (`Id` =	77	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tapologo', `LocationId`=33	  WHERE (`Id` =	78	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Extesion5', `LocationId`=33	  WHERE (`Id` =	79	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 1', `LocationId`=38	  WHERE (`Id` =	80	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 2', `LocationId`=38	  WHERE (`Id` =	81	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 3', `LocationId`=38	  WHERE (`Id` =	82	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 4', `LocationId`=38	  WHERE (`Id` =	83	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 5', `LocationId`=38	  WHERE (`Id` =	84	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 6', `LocationId`=38	  WHERE (`Id` =	85	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 7', `LocationId`=38	  WHERE (`Id` =	86	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 8', `LocationId`=38	  WHERE (`Id` =	87	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 9', `LocationId`=38	  WHERE (`Id` =	88	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 10', `LocationId`=38	  WHERE (`Id` =	89	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Unit 11', `LocationId`=38	  WHERE (`Id` =	90	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Industrial Zone', `LocationId`=38	  WHERE (`Id` =	91	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'BCL Plots', `LocationId`=89	  WHERE (`Id` =	92	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Botshabelo', `LocationId`=89	  WHERE (`Id` =	93	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Distance', `LocationId`=89	  WHERE (`Id` =	94	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Orlando', `LocationId`=89	  WHERE (`Id` =	95	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Tshabantsha', `LocationId`=89	  WHERE (`Id` =	96	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'New Stance', `LocationId`=89	  WHERE (`Id` =	97	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phase 1', `LocationId`=89	  WHERE (`Id` =	98	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phase 2', `LocationId`=89	  WHERE (`Id` =	99	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phase 3', `LocationId`=89	  WHERE (`Id` =	100	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Phase 4', `LocationId`=89	  WHERE (`Id` =	101	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Town Centre Selibe Phikwe', `LocationId`=89	  WHERE (`Id` =	102	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Kasane Botshabelo', `LocationId`=91	  WHERE (`Id` =	103	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'China Town', `LocationId`=91	  WHERE (`Id` =	104	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'New Town', `LocationId`=91	  WHERE (`Id` =	105	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Plateau', `LocationId`=91	  WHERE (`Id` =	106	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Kgaphamadi', `LocationId`=91	  WHERE (`Id` =	107	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'River Front', `LocationId`=91	  WHERE (`Id` =	108	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Pimville', `LocationId`=91	  WHERE (`Id` =	109	);
UPDATE `gosmartvalue`.`Localities` SET `Name` = 'Kgaphamadi Industrial', `LocationId`=91	  WHERE (`Id` =	110	);