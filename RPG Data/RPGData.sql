USE `rpg-game`;

DROP TABLE IF EXISTS `AttrTypes`;
CREATE TABLE `AttrTypes` (
  `attrTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `attrType` varchar(45) NOT NULL,
  `attrLowRange` int(11) NOT NULL,
  `attrHighRange` int(11) NOT NULL,
  PRIMARY KEY (`attrTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Attribute Types';

LOCK TABLES `AttrTypes` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `Inventory`;
CREATE TABLE `Inventory` (
  `invID` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) NOT NULL,
  `bagSlot` int(11) NOT NULL,
  `bagSlotLoc` int(11) NOT NULL,
  `itemID` int(11) NOT NULL,
  `invQuantity` int(11) NOT NULL,
  PRIMARY KEY (`invID`),
  KEY `PlayerInv_idx` (`userID`),
  KEY `InvItem_idx` (`itemID`),
  CONSTRAINT `InvItem` FOREIGN KEY (`itemID`) REFERENCES `ItemList` (`itemID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `PlayerInv` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Player Inventory';

LOCK TABLES `Inventory` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `ItemList`;
CREATE TABLE `ItemList` (
  `itemID` int(11) NOT NULL AUTO_INCREMENT,
  `itemName` varchar(32) NOT NULL,
  `itemType` int(11) NOT NULL,
  `itemNumAttr` int(11) DEFAULT NULL,
  `itemImage` varchar(45) NOT NULL,
  PRIMARY KEY (`itemID`),
  KEY `ItemType_idx` (`itemType`),
  CONSTRAINT `ItemType` FOREIGN KEY (`itemType`) REFERENCES `itemTypes` (`itemTypeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Default Items';

LOCK TABLES `ItemList` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `PlayerStats`;
CREATE TABLE `PlayerStats` (
  `playerID` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) NOT NULL,
  `playerXLoc` varchar(45) NOT NULL,
  `playerYLoc` varchar(45) NOT NULL,
  `playerHP` int(11) NOT NULL,
  `playerStr` int(11) NOT NULL,
  `playerInt` int(11) NOT NULL,
  `playerRace` int(11) NOT NULL,
  `playerSpec` int(11) NOT NULL,
  PRIMARY KEY (`playerID`),
  KEY `PlayerName_idx` (`userID`),
  KEY `PlayerRace_idx` (`playerRace`),
  KEY `PlayerSpec_idx` (`playerSpec`),
  CONSTRAINT `PlayerRace` FOREIGN KEY (`playerRace`) REFERENCES `race` (`raceID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `PlayerSpec` FOREIGN KEY (`playerSpec`) REFERENCES `specs` (`specID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `PlayerName` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Player Stats';

LOCK TABLES `PlayerStats` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `itemAttr`;
CREATE TABLE `itemAttr` (
  `itemattrID` int(11) NOT NULL AUTO_INCREMENT,
  `itemID` int(11) NOT NULL,
  `AttrType` int(11) NOT NULL,
  `AttrValue` int(11) NOT NULL,
  `AttrDesc` varchar(128) NOT NULL,
  PRIMARY KEY (`itemattrID`),
  KEY `InvAttr_idx` (`itemID`),
  CONSTRAINT `InvAttr` FOREIGN KEY (`itemID`) REFERENCES `ItemList` (`itemID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='ItemAttributes';

LOCK TABLES `itemAttr` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `itemTypes`;
CREATE TABLE `itemTypes` (
  `itemTypeID` int(11) NOT NULL,
  `itemTypeName` varchar(45) NOT NULL,
  `itemTypeAttrAllowed` enum('Y','N') NOT NULL,
  `NumAttrs` int(11) DEFAULT NULL,
  PRIMARY KEY (`itemTypeID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Types of Items';

LOCK TABLES `itemTypes` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `map-specials`;
CREATE TABLE `map-specials` (
  `mapspecID` int(11) NOT NULL AUTO_INCREMENT,
  `mapspName` varchar(45) NOT NULL,
  `mapspType` int(11) NOT NULL,
  `maplocX` int(11) NOT NULL,
  `maplocY` int(11) NOT NULL,
  PRIMARY KEY (`mapspecID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Map Specials - Dungeons, cities';

LOCK TABLES `map-specials` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `mapConfig`;
CREATE TABLE `mapConfig` (
  `mapID` int(11) NOT NULL AUTO_INCREMENT,
  `mapType` int(11) NOT NULL,
  `mapXsize` int(11) NOT NULL,
  `mapYsize` int(11) NOT NULL,
  `mapZsize` int(11) DEFAULT NULL,
  PRIMARY KEY (`mapID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COMMENT='Game map Configs';

LOCK TABLES `mapConfig` WRITE;
INSERT INTO `mapConfig` VALUES (1,1,150,150,3),(2,2,1500,1500,NULL),(3,3,75,75,10),(4,4,200,200,5),(5,5,50,50,NULL);
UNLOCK TABLES;

DROP TABLE IF EXISTS `mapData`;
CREATE TABLE `mapData` (
  `mapDataID` int(11) NOT NULL AUTO_INCREMENT,
  `mapID` int(11) NOT NULL,
  `mapX` int(11) NOT NULL,
  `mapY` int(11) NOT NULL,
  `mapZ` int(11) NOT NULL,
  `mapTiles` varchar(1024) NOT NULL,
  PRIMARY KEY (`mapDataID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Map Data';

LOCK TABLES `mapData` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `mapTypes`;
CREATE TABLE `mapTypes` (
  `mapTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `mapType` varchar(45) NOT NULL,
  `mapHasX` enum('Y','N') DEFAULT NULL,
  `mapHasY` enum('Y','N') DEFAULT NULL,
  `mapHasZ` enum('Y','N') DEFAULT NULL,
  PRIMARY KEY (`mapTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COMMENT='Map Types';

LOCK TABLES `mapTypes` WRITE;
INSERT INTO `mapTypes` VALUES (1,'City','Y','Y','Y'),(2,'World','Y','Y','N'),(3,'Dungeon','Y','Y','Y'),(4,'Capitol','Y','Y','Y'),(5,'Town','Y','Y','N');
UNLOCK TABLES;

DROP TABLE IF EXISTS `playerBags`;
CREATE TABLE `playerBags` (
  `bagID` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) NOT NULL,
  `bagSlot1` int(11) DEFAULT '0',
  `bagSlot2` int(11) DEFAULT '0',
  `bagSlot3` int(11) DEFAULT '0',
  PRIMARY KEY (`bagID`),
  KEY `PlayerName_idx` (`userID`),
  KEY `PlayerBags_idx` (`userID`),
  CONSTRAINT `PlayerBags` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Player Bag Bar';

LOCK TABLES `playerBags` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `race`;
CREATE TABLE `race` (
  `raceID` int(11) NOT NULL AUTO_INCREMENT,
  `raceName` varchar(45) DEFAULT NULL,
  `raceType` enum('M','P','B') DEFAULT NULL,
  PRIMARY KEY (`raceID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Races for players and mobs';

LOCK TABLES `race` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `specs`;
CREATE TABLE `specs` (
  `specID` int(11) NOT NULL,
  `specName` varchar(45) DEFAULT NULL,
  `specType` enum('M','P','B') DEFAULT NULL,
  PRIMARY KEY (`specID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Specs for players and mobs';

LOCK TABLES `specs` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(16) NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `password` varchar(32) NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `playerName` varchar(45) NOT NULL,
  PRIMARY KEY (`userID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

LOCK TABLES `user` WRITE;
UNLOCK TABLES;
