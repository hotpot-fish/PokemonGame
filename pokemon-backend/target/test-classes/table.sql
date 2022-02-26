DROP TABLE IF EXISTS `pokemon_skill`;
DROP TABLE IF EXISTS `player_pokemon`;
DROP TABLE IF EXISTS `skill_buff`;
DROP TABLE IF EXISTS `player_ball`;
DROP TABLE IF EXISTS `player_potion`;
DROP TABLE IF EXISTS `player`;
DROP TABLE IF EXISTS `pokemon`;
DROP TABLE IF EXISTS `skill`;
DROP TABLE IF EXISTS `buff`;
DROP TABLE IF EXISTS `ball`;
DROP TABLE IF EXISTS `potion`;

-- ----------------------------
-- Table structure for player
-- ----------------------------
CREATE TABLE `player` (
    `id` int AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `image` varchar(255),
    `account` varchar(255) NOT NULL UNIQUE,
    `password` varchar(255) NOT NULL,
    `email` varchar(255) NOT NULL,
    `rank_point` int NOT NULL default 1000,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for pokemon
-- ----------------------------
CREATE TABLE `pokemon` (
    `id` int AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `image` int NOT NULL,
    `element` varchar(255) NOT NULL,
    `base_attack` int NOT NULL,
    `base_defence` int NOT NULL,
    `base_HP` int NOT NULL,
    `base_speed` int NOT NULL,
    `grow_attack` decimal NOT NULL,
    `grow_defence` decimal NOT NULL,
    `grow_HP` decimal NOT NULL,
    `grow_speed` decimal NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for skill
-- ----------------------------
CREATE TABLE `skill` (
    `id` int AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `power` int NOT NULL,
    `element` varchar(255) NOT NULL,
    `max_pp` int NOT NULL,
    `priority` int NOT NULL,
    `learn_level` int NOT NULL,
    `describe` varchar(255) NOT NULL,
    `action` varchar(255) NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for buff
-- ----------------------------
CREATE TABLE `buff` (
    `id` int AUTO_INCREMENT,
    `disc_type` varchar(255),
    `effect` varchar(255),
    `data` int,
    `target_self` bool,
    `settle_time` varchar(255),
    `lasting` int,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for pokemon_skill
-- ----------------------------
CREATE TABLE `pokemon_skill` (
    `id` int AUTO_INCREMENT,
    `pokemon_id` int NOT NULL,
    `skill_id` int NOT NULL,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`pokemon_id`)
        REFERENCES pokemon(`id`) ON DELETE CASCADE,
    FOREIGN KEY (`skill_id`)
        REFERENCES skill(`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for player_pokemon
-- ----------------------------
CREATE TABLE `player_pokemon` (
    `id` int AUTO_INCREMENT,
    `player_id` int NOT NULL,
    `pokemon_id` int NOT NULL,
    `level` int NOT NULL,
    `bag_index` int NOT NULL,
    `cur_attack` int NOT NULL,
    `cur_defence` int NOT NULL,
    `cur_HP` int NOT NULL,
    `cur_speed` int NOT NULL,
    `skill1_id` int default NULL,
    `skill2_id` int default NULL,
    `skill3_id` int default NULL,
    `skill4_id` int default NULL,
    `exp` int,
    `cur_exp` int,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`player_id`)
        REFERENCES player(`id`) ON DELETE CASCADE,
    FOREIGN KEY (`pokemon_id`)
        REFERENCES pokemon(`id`) ON DELETE CASCADE,
    FOREIGN KEY (`skill1_id`)
        REFERENCES skill(`id`),
    FOREIGN KEY (`skill2_id`)
        REFERENCES skill(`id`),
    FOREIGN KEY (`skill3_id`)
        REFERENCES skill(`id`),
    FOREIGN KEY (`skill4_id`)
        REFERENCES skill(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for skill_buff
-- ----------------------------
CREATE TABLE `skill_buff` (
    `id` int AUTO_INCREMENT,
    `skill_id` int NOT NULL,
    `buff_id` int NOT NULL,
    `possibility` float NOT NULL,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`skill_id`)
        REFERENCES skill(`id`),
    FOREIGN KEY (`buff_id`)
        REFERENCES buff(`id`)
);

-- ----------------------------
-- Table structure for ball
-- ----------------------------
CREATE TABLE `ball` (
    `id` int AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `image` int NOT NULL,
    `possibility` float NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for potion
-- ----------------------------
CREATE TABLE `potion` (
    `id` int AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `type` varchar(255) NOT NULL,
    `data` int NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for player_ball
-- ----------------------------
CREATE TABLE `player_ball` (
    `id` int AUTO_INCREMENT,
    `player_id` int NOT NULL,
    `ball_id` int NOT NULL,
    `num` int NOT NULL,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`player_id`)
       REFERENCES player(`id`) ON DELETE CASCADE,
    FOREIGN KEY (`ball_id`)
      REFERENCES ball(`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for player_potion
-- ----------------------------
CREATE TABLE `player_potion` (
    `id` int AUTO_INCREMENT,
    `player_id` int NOT NULL,
    `potion_id` int NOT NULL,
    `num` int NOT NULL,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`player_id`)
       REFERENCES player(`id`) ON DELETE CASCADE,
    FOREIGN KEY (`potion_id`)
       REFERENCES potion(`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
