CREATE DATABASE  IF NOT EXISTS `nppes_1` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;
USE `nppes_1`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: 192.168.1.151    Database: nppes
-- ------------------------------------------------------
-- Server version	5.6.26-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `npi_organization_data`
--

DROP TABLE IF EXISTS `npi_organization_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `npi_organization_data` (
  `NPI` int(10) unsigned NOT NULL,
  `Name` varchar(70) COLLATE utf8_unicode_ci NOT NULL,
  `OtherName` varchar(70) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OtherNameTypeCode` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstLineMailingAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SecondLineMailingAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressCity` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressState` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressPostalCode` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressCountryCode` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressFax` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstLinePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SecondLinePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressCity` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressState` varchar(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressPostalCode` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressCountryCode` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressFax` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AuthorizedOfficialLastName` varchar(35) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AuthorizedOfficialFirstName` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AuthorizedOfficialTitle` varchar(35) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AuthorizedOfficialCredential` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AuthorizedOfficialTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TaxonomyCode1` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseNumber1` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseStateCode1` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TaxonomySwitch1` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsSoleProprietor` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsOrganizationSubpart` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DeactivationDate` datetime DEFAULT NULL,
  PRIMARY KEY (`NPI`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `npi_provider_data`
--

DROP TABLE IF EXISTS `npi_provider_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `npi_provider_data` (
  `NPI` int(10) unsigned NOT NULL,
  `ProviderLastName` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProviderFirstName` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProviderNamePrefix` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProviderNameSuffix` varchar(5) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ProviderCredentialText` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstLineMailingAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SecondLineMailingAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressCity` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressState` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressPostalCode` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressCountryCode` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MailingAddressFax` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FirstLinePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SecondLinePracticeAddress` varchar(55) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressCity` varchar(45) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressPostalCode` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressCountryCode` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressTelephone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PracticeAddressFaxNumber` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TaxonomyCode1` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseNumber1` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LicenseStateCode1` varchar(2) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TaxonomySwitch1` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsSoleProprietor` varchar(1) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DeactivationDate` datetime DEFAULT NULL,
  PRIMARY KEY (`NPI`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;


/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-11-28 11:07:48
