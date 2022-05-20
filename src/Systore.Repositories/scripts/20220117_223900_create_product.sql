CREATE TABLE `product` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SaleType` tinyint(4) NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `ExpirationDays` smallint(6) NOT NULL,
  `Description` varchar(30) CHARACTER SET utf8mb4 DEFAULT NULL,
  `ExtraInformation` text,
  `PrintExpirationDate` tinyint(4) NOT NULL,
  `PrintDateOfPackaging` tinyint(4) NOT NULL,
  `ExportToBalance` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
