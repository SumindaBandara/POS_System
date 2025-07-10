-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 19, 2024 at 11:54 AM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pizzadb`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `id` int(50) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id`, `username`, `password`) VALUES
(1, 'admin', '1234');

-- --------------------------------------------------------

--
-- Table structure for table `kitchen`
--

CREATE TABLE `kitchen` (
  `Id` int(255) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Regular` int(100) NOT NULL,
  `Medium` int(100) NOT NULL,
  `Large` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `kitchen`
--

INSERT INTO `kitchen` (`Id`, `Name`, `Regular`, `Medium`, `Large`) VALUES
(1, 'Flor', 100, 50, 70),
(2, 'Cheese', 150, 80, 100),
(3, 'Chicken', 100, 150, 70),
(4, 'Vegetable', 50, 150, 100);

-- --------------------------------------------------------

--
-- Table structure for table `pizzamenu`
--

CREATE TABLE `pizzamenu` (
  `Id` int(255) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `RegularPrice` int(100) NOT NULL,
  `MediumPrice` int(100) NOT NULL,
  `LargePrice` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pizzamenu`
--

INSERT INTO `pizzamenu` (`Id`, `Name`, `RegularPrice`, `MediumPrice`, `LargePrice`) VALUES
(1, 'Chicken Pizza', 1000, 1500, 2100),
(2, 'Cheese Pizza', 1050, 1550, 2100),
(3, 'Roast Chicken Pizza', 1150, 1650, 2150),
(4, 'Mushroom Pizza', 950, 1400, 1900),
(5, 'BBQ Pizza', 1250, 1750, 2300),
(6, 'Burger Pizza', 1100, 1600, 2050),
(7, 'Sosage Pizza', 950, 1450, 1900),
(8, 'Egg Pizza', 1000, 1550, 2100),
(9, 'Vegetable Pizza', 1050, 1550, 2100);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `kitchen`
--
ALTER TABLE `kitchen`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `pizzamenu`
--
ALTER TABLE `pizzamenu`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `kitchen`
--
ALTER TABLE `kitchen`
  MODIFY `Id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `pizzamenu`
--
ALTER TABLE `pizzamenu`
  MODIFY `Id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
