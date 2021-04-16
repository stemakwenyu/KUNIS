/*
Navicat MySQL Data Transfer

Source Server         : conn
Source Server Version : 50508
Source Host           : localhost:3306
Source Database       : kunis_db

Target Server Type    : MYSQL
Target Server Version : 50508
File Encoding         : 65001

Date: 2018-06-14 10:30:17
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `bodyformats`
-- ----------------------------
DROP TABLE IF EXISTS `bodyformats`;
CREATE TABLE `bodyformats` (
  `Format` int(11) NOT NULL DEFAULT '0',
  `Description` varchar(255) DEFAULT NULL,
  `DescriptionEx` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Format`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of bodyformats
-- ----------------------------
INSERT INTO `bodyformats` VALUES ('0', 'Text', 'Text');
INSERT INTO `bodyformats` VALUES ('1', 'Text (F)', 'Text - Flash');
INSERT INTO `bodyformats` VALUES ('2', 'Data', 'Data');
INSERT INTO `bodyformats` VALUES ('3', 'Data (UDH)', 'Data - User Data Header');
INSERT INTO `bodyformats` VALUES ('4', 'Unicode', 'Unicode');
INSERT INTO `bodyformats` VALUES ('5', 'Unicode (F)', 'Unicode - Flash');
INSERT INTO `bodyformats` VALUES ('6', 'WAP Push', 'WAP Push');
INSERT INTO `bodyformats` VALUES ('7', 'WAP Bookmark', 'WAP Bookmark');

-- ----------------------------
-- Table structure for `department`
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department` (
  `Department_ID` varchar(12) NOT NULL DEFAULT '',
  `Department_Name` varchar(255) DEFAULT NULL,
  `Room` varchar(255) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `School_ID` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`Department_ID`),
  KEY `schCode` (`School_ID`),
  CONSTRAINT `schCode` FOREIGN KEY (`School_ID`) REFERENCES `school` (`School_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of department
-- ----------------------------
INSERT INTO `department` VALUES ('CS', 'COMPUTER SCIENCE', 'ABC 319', 'OFFERS COMPUTER', 'SCAI');
INSERT INTO `department` VALUES ('IT', 'INFORMATION TECHNOLOGY', 'ABC 308', 'NEW BLOCK', 'SCAI');

-- ----------------------------
-- Table structure for `directions`
-- ----------------------------
DROP TABLE IF EXISTS `directions`;
CREATE TABLE `directions` (
  `Direction` int(11) NOT NULL DEFAULT '0',
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Direction`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of directions
-- ----------------------------
INSERT INTO `directions` VALUES ('0', 'Undefined');
INSERT INTO `directions` VALUES ('1', 'In');
INSERT INTO `directions` VALUES ('2', 'Out');

-- ----------------------------
-- Table structure for `messages`
-- ----------------------------
DROP TABLE IF EXISTS `messages`;
CREATE TABLE `messages` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Direction` int(11) NOT NULL DEFAULT '0',
  `Type` int(11) NOT NULL DEFAULT '0',
  `StatusDetails` int(11) NOT NULL DEFAULT '0',
  `Status` int(11) NOT NULL DEFAULT '0',
  `ChannelID` int(11) NOT NULL DEFAULT '0',
  `MessageReference` varchar(255) DEFAULT '',
  `SentTimeSecs` int(11) NOT NULL DEFAULT '0',
  `ReceivedTimeSecs` int(11) NOT NULL DEFAULT '0',
  `ScheduledTimeSecs` int(11) NOT NULL DEFAULT '0',
  `LastUpdateSecs` int(11) NOT NULL DEFAULT '0',
  `Sender` varchar(255) DEFAULT '',
  `Recipient` varchar(255) DEFAULT '',
  `Subject` varchar(255) DEFAULT '',
  `BodyFormat` int(11) NOT NULL DEFAULT '0',
  `CustomField1` int(11) NOT NULL DEFAULT '0',
  `CustomField2` varchar(255) DEFAULT '',
  `sysCreator` int(11) NOT NULL DEFAULT '0',
  `sysArchive` tinyint(1) NOT NULL DEFAULT '0',
  `sysLock` tinyint(1) NOT NULL DEFAULT '0',
  `sysHash` varchar(255) DEFAULT '',
  `sysForwarded` tinyint(1) NOT NULL DEFAULT '0',
  `sysGwReference` varchar(255) DEFAULT '',
  `Header` text,
  `Body` text,
  `Trace` text,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of messages
-- ----------------------------

-- ----------------------------
-- Table structure for `programme`
-- ----------------------------
DROP TABLE IF EXISTS `programme`;
CREATE TABLE `programme` (
  `Prog_ID` varchar(12) NOT NULL DEFAULT '',
  `Prog_Name` varchar(255) DEFAULT NULL,
  `Duration` double(10,2) DEFAULT NULL,
  `Department_ID` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`Prog_ID`),
  KEY `depID` (`Department_ID`),
  CONSTRAINT `depID` FOREIGN KEY (`Department_ID`) REFERENCES `department` (`Department_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of programme
-- ----------------------------
INSERT INTO `programme` VALUES ('COM', 'Bsc. Computer Science', '4.00', 'CS');

-- ----------------------------
-- Table structure for `school`
-- ----------------------------
DROP TABLE IF EXISTS `school`;
CREATE TABLE `school` (
  `School_ID` varchar(12) NOT NULL DEFAULT '',
  `School_Name` varchar(255) DEFAULT NULL,
  `Room` varchar(255) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`School_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of school
-- ----------------------------
INSERT INTO `school` VALUES ('FESS', 'FACULTY OF EDUCATION AND SOCIAL SCIENCES', 'ABB 114', 'OLD BLOCK');
INSERT INTO `school` VALUES ('SCAI', 'School of Computing and Informatics', 'ABC 312', 'NEW BLOCK');

-- ----------------------------
-- Table structure for `status`
-- ----------------------------
DROP TABLE IF EXISTS `status`;
CREATE TABLE `status` (
  `Status` int(11) NOT NULL DEFAULT '0',
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Status`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of status
-- ----------------------------
INSERT INTO `status` VALUES ('0', 'Undefined');
INSERT INTO `status` VALUES ('1', 'Pending');
INSERT INTO `status` VALUES ('2', 'Success');
INSERT INTO `status` VALUES ('3', 'Failure');

-- ----------------------------
-- Table structure for `statusdetails`
-- ----------------------------
DROP TABLE IF EXISTS `statusdetails`;
CREATE TABLE `statusdetails` (
  `StatusDetails` int(11) NOT NULL DEFAULT '0',
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`StatusDetails`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of statusdetails
-- ----------------------------
INSERT INTO `statusdetails` VALUES ('0', '');
INSERT INTO `statusdetails` VALUES ('100', 'Received; waiting to be processed');
INSERT INTO `statusdetails` VALUES ('101', 'Received; processed successfully');
INSERT INTO `statusdetails` VALUES ('102', 'Received; no processing required, no triggers defined');
INSERT INTO `statusdetails` VALUES ('103', 'Received; processing failure');
INSERT INTO `statusdetails` VALUES ('104', 'Received; no processing required, no trigger condition matched');
INSERT INTO `statusdetails` VALUES ('110', 'Received; processing failure');
INSERT INTO `statusdetails` VALUES ('200', 'Scheduled');
INSERT INTO `statusdetails` VALUES ('201', 'Queued');
INSERT INTO `statusdetails` VALUES ('202', 'Submitted; waiting for ACK');
INSERT INTO `statusdetails` VALUES ('210', 'Generic error');
INSERT INTO `statusdetails` VALUES ('211', 'No channel can handle this message');
INSERT INTO `statusdetails` VALUES ('212', 'Message undeliverable');
INSERT INTO `statusdetails` VALUES ('213', 'NACK received');
INSERT INTO `statusdetails` VALUES ('214', 'ACK expired');
INSERT INTO `statusdetails` VALUES ('215', 'Duplicate ACK detected');
INSERT INTO `statusdetails` VALUES ('220', 'Sent');
INSERT INTO `statusdetails` VALUES ('221', 'Delivered; ACK received');
INSERT INTO `statusdetails` VALUES ('255', 'Locked by system');

-- ----------------------------
-- Table structure for `student`
-- ----------------------------
DROP TABLE IF EXISTS `student`;
CREATE TABLE `student` (
  `Reg_No` varchar(30) NOT NULL DEFAULT '',
  `Student_Name` varchar(255) DEFAULT NULL,
  `Phone_No` varchar(60) DEFAULT NULL,
  `DoB` date DEFAULT NULL,
  `Gender` varchar(20) DEFAULT NULL,
  `Email_Address` varchar(255) DEFAULT NULL,
  `Postal_Address` varchar(255) DEFAULT NULL,
  `Photo` varchar(600) DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `Prog_ID` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`Reg_No`),
  KEY `progID` (`Prog_ID`),
  CONSTRAINT `progID` FOREIGN KEY (`Prog_ID`) REFERENCES `programme` (`Prog_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of student
-- ----------------------------
INSERT INTO `student` VALUES ('BIT/0001/19', 'JOSEPH', '+254736789456', '2009-05-04', 'Female', 'joseph@kibu.ac.ke', '1699-50200 Bungoma', null, '0', 'COM');
INSERT INTO `student` VALUES ('COM/0007/18', 'JACK', '+254726383112', '2018-06-14', 'Male', 'jack@gmail.com', '1699-50200 Bungoma', '', '1', 'COM');
INSERT INTO `student` VALUES ('EDA/1024/17', 'CAROLYNE WAMALWA', '+254712789456', '2018-06-12', 'Female', 'caro@yahoo.com', 'czvxzc', 'E:__KUNIS__Photos__6817.JPG', '1', 'COM');

-- ----------------------------
-- Table structure for `type`
-- ----------------------------
DROP TABLE IF EXISTS `type`;
CREATE TABLE `type` (
  `Type` int(11) NOT NULL DEFAULT '0',
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of type
-- ----------------------------
INSERT INTO `type` VALUES ('0', 'Undefined');
INSERT INTO `type` VALUES ('1', 'E-mail');
INSERT INTO `type` VALUES ('2', 'Sms');
INSERT INTO `type` VALUES ('3', 'File');

-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `User_ID` varchar(30) NOT NULL DEFAULT '',
  `User_Name` varchar(255) DEFAULT NULL,
  `Login_Name` varchar(20) DEFAULT NULL,
  `Passsword` varchar(60) DEFAULT NULL,
  `Status` tinyint(1) DEFAULT NULL,
  `Priviledges` varchar(255) DEFAULT NULL,
  `Phone_No` varchar(60) DEFAULT NULL,
  PRIMARY KEY (`User_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('EMP/0001', 'System Admin', 'adm', 'adm', '1', 'Admin', '+254723606988');
INSERT INTO `user` VALUES ('EMP/0002', 'jack', 'jack', 'jack', '1', 'Clerk', '0726383112');
