-- ----------------------------
-- Data for pokemon
-- ----------------------------

INSERT INTO `pokemon` VALUES ('1','小蜜蜂','1','飞行',
                              '10','8','120','10',
                              '3','2.5','3.5','3');
INSERT INTO `pokemon` VALUES ('2','蜘蛛王','2','虫',
                              '11','7','140','9',
                              '3.3','2.35','3.7','2.9');
INSERT INTO `pokemon` VALUES ('3','蝙蝠领主','3','飞行',
                              '10','6','130','11',
                              '3.08','2.3','3.55','3.08');
INSERT INTO `pokemon` VALUES ('4','小幽灵','4','恶魔',
                              '10','7','130','10',
                              '3.1','2.3','3.5','3.05');
INSERT INTO `pokemon` VALUES ('5','蘑菇怪','5','草',
                              '8','10','50','8',
                              '2.7','4.9','2','2.8');
INSERT INTO `pokemon` VALUES ('6','墨影人','6','岩',
                              '9','8','135','10',
                              '3','2.5','3.6','2.8');
INSERT INTO `pokemon` VALUES ('7','咕咕鸟','7','飞行',
                              '10','8','120','10',
                              '2.9','2.45','3.55','3.08');
INSERT INTO `pokemon` VALUES ('8','小鸡','8','普通',
                              '9','7','125','9',
                              '3','2.45','3.65','3');
INSERT INTO `pokemon` VALUES ('9','小豆芽','9','草',
                              '9','7','130','10',
                              '3','2.5','3.6','2.9');

-- ----------------------------
-- Data for skill
-- ----------------------------

-- ------普通系技能------ --
-- 基础攻击技能
INSERT INTO `skill` VALUES ('1','撞击','35','普通','20','0','1','普通攻击','攻击');
INSERT INTO `skill` VALUES ('2','猛冲','40','普通','15','0','1','普通攻击','攻击');
INSERT INTO `skill` VALUES ('3','乱突','60','普通','15','0','20','普通攻击','攻击');
INSERT INTO `skill` VALUES ('4','抓','60','普通','20','0','20','普通攻击','攻击');
INSERT INTO `skill` VALUES ('5','冲顶','60','普通','20','0','20','普通攻击','攻击');
-- buff技能
INSERT INTO `skill` VALUES ('11','瞪眼','0','普通','20','0','1','使对方攻击减1','属性');
INSERT INTO `skill` VALUES ('12','意念','0','普通','20','0','1','使对方防御减1','属性');
INSERT INTO `skill` VALUES ('13','铁壁','0','普通','20','0','1','使自己防御加1','属性');
INSERT INTO `skill` VALUES ('14','蓄力','0','普通','15','0','1','使自己攻击加1','属性');
INSERT INTO `skill` VALUES ('15','诅咒','0','普通','15','0','1','使对方速度减1','属性');
INSERT INTO `skill` VALUES ('16','疾跑','0','普通','20','0','1','使自己速度加1','属性');
INSERT INTO `skill` VALUES ('17','神秘守护','0','普通','10','4','60','先制+4，接下来5回合免疫控制','属性');
-- 进阶攻击技能
INSERT INTO `skill` VALUES ('21','舍身撞击','120','普通','5','0','60','舍身一击，自己也受到25%的伤害','攻击');
INSERT INTO `skill` VALUES ('22','泰山压顶','80','普通','10','0','60','普通攻击','攻击');
INSERT INTO `skill` VALUES ('23','飞踢','70','普通','15','0','60','20%使自己攻击加1','攻击');
INSERT INTO `skill` VALUES ('24','回击','100','普通','10','-1','60','先制-1','攻击');
INSERT INTO `skill` VALUES ('25','回旋踢','80','普通','10','0','60','普通攻击','攻击');
INSERT INTO `skill` VALUES ('26','闪光波','80','普通','15','0','60','普通攻击','攻击');
INSERT INTO `skill` VALUES ('27','突进','80','普通','15','1','60','先制+1','攻击');
INSERT INTO `skill` VALUES ('28','全力一击','200','普通','5','0','80','使自己攻击-1','攻击');
INSERT INTO `skill` VALUES ('29','克制','0','普通','5','-1','80','先制-1，双倍反弹对手当回合造成的伤害','攻击');

-- ------飞行系技能------ --
-- 基础攻击技能
INSERT INTO `skill` VALUES ('31','风刃','40','飞行','20','0','1','飞行系攻击','攻击');
INSERT INTO `skill` VALUES ('32','吹风','45','飞行','15','0','1','飞行系攻击','攻击');
INSERT INTO `skill` VALUES ('33','飞击','40','飞行','25','0','1','飞行系攻击','攻击');
INSERT INTO `skill` VALUES ('34','旋风','50','飞行','15','0','15','飞行系攻击','攻击');
INSERT INTO `skill` VALUES ('35','风切术','50','飞行','20','0','15','飞行系攻击','攻击');
INSERT INTO `skill` VALUES ('36','羽刃','65','飞行','15','0','35','10%使对方速度-1','攻击');
INSERT INTO `skill` VALUES ('37','翼魂','65','飞行','15','0','35','10%使自己速度+1','攻击');
-- buff技能
INSERT INTO `skill` VALUES ('41','高速移动','0','飞行','15','0','30','使自己速度+2','属性');
INSERT INTO `skill` VALUES ('42','空气墙','0','飞行','20','0','30','使对方速度-2','属性');
INSERT INTO `skill` VALUES ('43','炫目突袭','0','飞行','5','0','70','使对方晕眩2回合','属性');
INSERT INTO `skill` VALUES ('44','飞天','0','飞行','10','1','70','先制+1，闪避对手当回合技能','属性');
INSERT INTO `skill` VALUES ('45','折翼求生','0','飞行','1','0','70','使自己防御-5，下5回合每回合恢复60%血量','属性');
-- 进阶攻击技能
INSERT INTO `skill` VALUES ('51','高空偷袭','80','飞行','10','1','60','先制+1','攻击');
INSERT INTO `skill` VALUES ('52','风暴冲撞','70','飞行','10','0','60','消除对方能力强化状态','攻击');
INSERT INTO `skill` VALUES ('53','飓风咆哮','70','飞行','10','0','60','解除自身能力下降状态','攻击');
INSERT INTO `skill` VALUES ('54','飞空斩击','100','飞行','10','0','70','飞行系攻击','攻击');
INSERT INTO `skill` VALUES ('55','风卷残云','80','飞行','10','0','70','20%吸血','攻击');
INSERT INTO `skill` VALUES ('56','破空长鸣','120','飞行','5','0','85','10%使自己速度+1','攻击');
INSERT INTO `skill` VALUES ('57','裂空冲袭','150','飞行','5','0','85','飞行系攻击','攻击');

-- ------虫系技能------ --
-- 基础攻击技能
INSERT INTO `skill` VALUES ('61','爪击','40','虫','20','0','1','虫系攻击','攻击');
INSERT INTO `skill` VALUES ('62','撕咬','40','虫','20','0','1','虫系攻击','攻击');
INSERT INTO `skill` VALUES ('63','毒针','50','虫','20','0','15','10%使对方中毒','攻击');
-- buff技能
INSERT INTO `skill` VALUES ('71','结网','0','虫','20','0','25','使对方速度-2','属性');
INSERT INTO `skill` VALUES ('72','腐蚀','0','虫','10','0','35','使对方攻击-1防御-1','属性');
INSERT INTO `skill` VALUES ('73','蛊虫','0','虫','15','0','35','使对方中毒','属性');
INSERT INTO `skill` VALUES ('74','流萤辉魄','0','虫','1','0','80','恢复自己全部血量','属性');
-- -- 进阶攻击技能
INSERT INTO `skill` VALUES ('81','虫群袭','90','虫','10','0','65','虫系攻击','攻击');
INSERT INTO `skill` VALUES ('82','神经毒素','90','虫','15','0','70','虫系攻击','攻击');
INSERT INTO `skill` VALUES ('83','幽鳞毒体','100','虫','10','0','80','20%使对方中毒','攻击');
INSERT INTO `skill` VALUES ('84','破茧杀戮','180','虫','5','0','85','虫系攻击','攻击');

-- ------恶魔系技能------ --
-- 基础攻击技能
INSERT INTO `skill` VALUES ('91','暗影拳','40','恶魔','20','0','1','恶魔系攻击','攻击');
INSERT INTO `skill` VALUES ('92','破幽斩','40','恶魔','20','0','1','恶魔系攻击','攻击');
-- buff技能
INSERT INTO `skill` VALUES ('101','夜幕降临','0','恶魔','10','0','20','使对方防御-2','属性');
INSERT INTO `skill` VALUES ('102','黑洞','0','恶魔','5','0','40','使对方疲惫2回合','属性');
INSERT INTO `skill` VALUES ('103','恶魔之拥','0','恶魔','3','0','80','恢复50%血量，并且使自己攻击+1','属性');
-- -- 进阶攻击技能
INSERT INTO `skill` VALUES ('111','恶魔恶魔恶魔','90','恶魔','10','0','60','恶魔系攻击','攻击');
INSERT INTO `skill` VALUES ('112','暗影突袭','90','恶魔','10','1','75','先制+1','攻击');
INSERT INTO `skill` VALUES ('113','修罗天谴','50','恶魔','10','0','85','下3回合每回合造成60点固定伤害','攻击');
INSERT INTO `skill` VALUES ('114','摄魂夺魄','100','恶魔','10','0','90','50%吸血','攻击');

-- ------草系技能------ --
-- 基础攻击技能
INSERT INTO `skill` VALUES ('121','针刺','40','草','20','0','1','草系攻击','攻击');
INSERT INTO `skill` VALUES ('122','飞叶快刀','40','草','20','0','1','草系攻击','攻击');
-- buff技能
INSERT INTO `skill` VALUES ('131','催眠粉','0','草','10','0','30','使对方晕眩2回合','属性');
INSERT INTO `skill` VALUES ('132','光合作用','0','草','5','0','40','下3回合每回合恢复20%的生命值','属性');
INSERT INTO `skill` VALUES ('133','绿叶护体','0','草','2','0','60','使自己防御+3','属性');
INSERT INTO `skill` VALUES ('134','春暖花开','0','草','3','0','80','恢复50点血量，并且使自己防御+1','属性');
INSERT INTO `skill` VALUES ('135','荆棘护体','0','草','3','0','80','下3回合每回合反弹25%伤害','属性');
INSERT INTO `skill` VALUES ('136','一莲托生','0','草','1','0','80','下3回合每回合恢复对方造成伤害50%的血量','属性');
-- -- 进阶攻击技能
INSERT INTO `skill` VALUES ('141','飞叶风暴','80','草','10','0','70','草系攻击','攻击');
INSERT INTO `skill` VALUES ('142','青山不语','80','草','5','0','85','20%使对方失明3回合','攻击');
INSERT INTO `skill` VALUES ('143','莽林狂舞','120','草','5','0','85','20%使自己防御+1','攻击');
INSERT INTO `skill` VALUES ('144','木灵束缚','80','草','5','0','75','消除对方能力强化状态','攻击');

-- ------岩系技能------ --
INSERT INTO `skill` VALUES ('151','岩爆','0','岩','20','0','1','额外对敌方造成40点固定伤害','攻击');
INSERT INTO `skill` VALUES ('152','岩石拳','0','岩','20','0','30','额外对敌方造成60点固定伤害','攻击');
-- buff技能
INSERT INTO `skill` VALUES ('161','石化皮肤','0','岩','20','0','20','使自己防御+2','属性');
-- -- 进阶攻击技能
INSERT INTO `skill` VALUES ('171','大地震击','0','岩','1','0','85','额外对敌方造成250点固定伤害','攻击');
INSERT INTO `skill` VALUES ('172','大地无极','0','岩','1','0','85','减少敌方40%HP','攻击');

-- ----------------------------
-- Data for buff
-- ----------------------------

-- Ability buff
INSERT INTO `buff` VALUES ('1','能力','攻击','1','1','立即',null);
INSERT INTO `buff` VALUES ('2','能力','防御','1','1','立即',null);
INSERT INTO `buff` VALUES ('3','能力','速度','1','1','立即',null);
INSERT INTO `buff` VALUES ('4','能力','攻击','2','1','立即',null);
INSERT INTO `buff` VALUES ('5','能力','防御','2','1','立即',null);
INSERT INTO `buff` VALUES ('6','能力','速度','2','1','立即',null);
INSERT INTO `buff` VALUES ('7','能力','攻击','-1','1','立即',null);
INSERT INTO `buff` VALUES ('8','能力','防御','-1','1','立即',null);
INSERT INTO `buff` VALUES ('9','能力','速度','-1','1','立即',null);
INSERT INTO `buff` VALUES ('10','能力','攻击','-2','1','立即',null);
INSERT INTO `buff` VALUES ('11','能力','防御','-2','1','立即',null);
INSERT INTO `buff` VALUES ('12','能力','速度','-2','1','立即',null);
INSERT INTO `buff` VALUES ('13','能力','攻击','1','0','立即',null);
INSERT INTO `buff` VALUES ('14','能力','防御','1','0','立即',null);
INSERT INTO `buff` VALUES ('15','能力','速度','1','0','立即',null);
INSERT INTO `buff` VALUES ('16','能力','攻击','2','0','立即',null);
INSERT INTO `buff` VALUES ('17','能力','防御','2','0','立即',null);
INSERT INTO `buff` VALUES ('18','能力','速度','2','0','立即',null);
INSERT INTO `buff` VALUES ('19','能力','攻击','-1','0','立即',null);
INSERT INTO `buff` VALUES ('20','能力','防御','-1','0','立即',null);
INSERT INTO `buff` VALUES ('21','能力','速度','-1','0','立即',null);
INSERT INTO `buff` VALUES ('22','能力','攻击','-2','0','立即',null);
INSERT INTO `buff` VALUES ('23','能力','防御','-2','0','立即',null);
INSERT INTO `buff` VALUES ('24','能力','速度','-2','0','立即',null);
INSERT INTO `buff` VALUES ('25','能力','消强','0','0','立即',null);
INSERT INTO `buff` VALUES ('26','能力','解弱','0','1','立即',null);
INSERT INTO `buff` VALUES ('53','能力','防御','3','1','立即',null);
INSERT INTO `buff` VALUES ('59','能力','防御','-5','1','立即',null);

-- Control buff
INSERT INTO `buff` VALUES ('31','控制','晕眩',null,'0','开始','3');
INSERT INTO `buff` VALUES ('32','控制','疲惫',null,'0','开始','3');
INSERT INTO `buff` VALUES ('33','控制','失明',null,'0','开始','3');
INSERT INTO `buff` VALUES ('34','控制','免控',null,'1','立即','6');
INSERT INTO `buff` VALUES ('35','控制','闪避',null,'1','立即','1');

-- HP buff
INSERT INTO `buff` VALUES ('41','血量','烧伤','-10','0','结束','3');
INSERT INTO `buff` VALUES ('42','血量','冻伤','-10','0','结束','3');
INSERT INTO `buff` VALUES ('43','血量','中毒','-10','0','结束','3');
INSERT INTO `buff` VALUES ('44','血量','固定','-80','0','结束','1');
INSERT INTO `buff` VALUES ('45','血量','百分比','100','1','结束','1');
INSERT INTO `buff` VALUES ('46','血量','己方伤害百分比','-25','0','结束','3');
INSERT INTO `buff` VALUES ('47','血量','己方伤害百分比','-25','1','立即','1');
INSERT INTO `buff` VALUES ('48','血量','己方伤害百分比','20','1','立即','1');
INSERT INTO `buff` VALUES ('49','血量','固定','-60','0','结束','3');
INSERT INTO `buff` VALUES ('50','血量','己方伤害百分比','50','1','立即','1');
INSERT INTO `buff` VALUES ('51','血量','百分比','50','1','结束','1');
INSERT INTO `buff` VALUES ('52','血量','百分比','20','1','结束','3');
INSERT INTO `buff` VALUES ('54','血量','固定','50','1','结束','1');
INSERT INTO `buff` VALUES ('55','血量','固定','-250','0','结束','1');
INSERT INTO `buff` VALUES ('56','血量','百分比','-40','0','结束','1');
INSERT INTO `buff` VALUES ('57','血量','固定','-40','0','结束','1');
INSERT INTO `buff` VALUES ('58','血量','固定','-60','0','结束','1');
INSERT INTO `buff` VALUES ('60','血量','百分比','60','1','结束','5');
INSERT INTO `buff` VALUES ('61','血量','己方伤害百分比','-200','0','立即','1');
INSERT INTO `buff` VALUES ('62','血量','敌方伤害百分比','50','1','结束','3');

-- ----------------------------
-- Data for pokemon_skill
-- ----------------------------

-- 小蜜蜂
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('1','1'),('1','11'),('1','14'),('1','21'),('1','27'),
       ('1','31'),('1','37'),('1','41'),('1','52'),('1','55'),
       ('1','56'),('1','81');

-- 蜘蛛王
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('2','2'),('2','15'),('2','17'),('2','22'),('2','63'),
       ('2','72'),('2','73'),('2','74'),('2','82'),('2','83'),
       ('2','84');

-- 小蝙蝠
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('3','32'),('3','33'),('3','34'),('3','36'),('3','42'),
       ('3','43'),('3','51'),('3','53'),('3','54'),('3','56'),
       ('3','57');

-- 小幽灵
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('4','1'),('4','13'),('4','91'),('4','92'),('4','101'),
       ('4','102'),('4','103'),('4','111'),('4','112'),('4','113'),
       ('4','114');

-- 蘑菇怪
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('5','5'),('5','11'),('5','17'),('5','121'),('5','131'),
       ('5','132'),('5','133'),('5','136'),('5','141'),('5','142');

-- 墨影人
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('6','17'),('6','151'),('6','152'),('6','161'),('6','171'),
       ('6','172');

-- 咕咕鸟
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('7','1'),('7','17'),('7','31'),('7','34'),('7','37'),
       ('7','44'),('7','45'),('7','52'),('7','57');

-- 小鸟
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('8','1'),('8','4'),('8','11'),('8','16'),('8','17'),
       ('8','21'),('8','27'),('8','28'),('8','29');

-- 小豆芽
INSERT INTO `pokemon_skill` (pokemon_id, skill_id)
VALUES ('9','1'),('9','122'),('9','131'),('9','132'),('9','135'),
       ('9','134'),('9','141'),('9','143'),('9','144');

-- ----------------------------
-- Data for skill_buff
-- ----------------------------

INSERT INTO `skill_buff` VALUES ('1','11','19','1');
INSERT INTO `skill_buff` VALUES ('2','12','20','1');
INSERT INTO `skill_buff` VALUES ('3','13','2','1');
INSERT INTO `skill_buff` VALUES ('4','14','1','1');
INSERT INTO `skill_buff` VALUES ('5','15','21','1');
INSERT INTO `skill_buff` VALUES ('6','16','3','1');
INSERT INTO `skill_buff` VALUES ('7','21','47','1');
INSERT INTO `skill_buff` VALUES ('8','23','1','0.2');
INSERT INTO `skill_buff` VALUES ('9','36','21','0.1');
INSERT INTO `skill_buff` VALUES ('10','37','3','0.1');
INSERT INTO `skill_buff` VALUES ('11','41','6','1');
INSERT INTO `skill_buff` VALUES ('12','42','24','1');
INSERT INTO `skill_buff` VALUES ('13','52','25','1');
INSERT INTO `skill_buff` VALUES ('14','53','26','1');
INSERT INTO `skill_buff` VALUES ('15','55','48','1');
INSERT INTO `skill_buff` VALUES ('16','56','3','0.1');
INSERT INTO `skill_buff` VALUES ('17','63','43','0.1');
INSERT INTO `skill_buff` VALUES ('18','71','24','1');
INSERT INTO `skill_buff` VALUES ('19','72','19','1');
INSERT INTO `skill_buff` VALUES ('20','72','20','1');
INSERT INTO `skill_buff` VALUES ('21','73','43','1');
INSERT INTO `skill_buff` VALUES ('22','74','45','1');
INSERT INTO `skill_buff` VALUES ('23','83','43','0.2');
INSERT INTO `skill_buff` VALUES ('25','43','31','1');
INSERT INTO `skill_buff` VALUES ('26','101','23','1');
INSERT INTO `skill_buff` VALUES ('27','102','32','1');
INSERT INTO `skill_buff` VALUES ('28','113','49','1');
INSERT INTO `skill_buff` VALUES ('29','114','50','1');
INSERT INTO `skill_buff` VALUES ('30','103','51','1');
INSERT INTO `skill_buff` VALUES ('31','103','1','1');
INSERT INTO `skill_buff` VALUES ('32','131','31','1');
INSERT INTO `skill_buff` VALUES ('33','132','52','1');
INSERT INTO `skill_buff` VALUES ('34','133','53','1');
INSERT INTO `skill_buff` VALUES ('35','134','2','1');
INSERT INTO `skill_buff` VALUES ('36','134','54','1');
INSERT INTO `skill_buff` VALUES ('37','142','33','0.2');
INSERT INTO `skill_buff` VALUES ('38','161','5','1');
INSERT INTO `skill_buff` VALUES ('39','171','55','1');
INSERT INTO `skill_buff` VALUES ('40','172','56','1');
INSERT INTO `skill_buff` VALUES ('41','44','35','1');
INSERT INTO `skill_buff` VALUES ('42','151','57','1');
INSERT INTO `skill_buff` VALUES ('43','152','58','1');
INSERT INTO `skill_buff` VALUES ('44','45','59','1');
INSERT INTO `skill_buff` VALUES ('45','45','60','1');
INSERT INTO `skill_buff` VALUES ('46','17','34','1');
INSERT INTO `skill_buff` VALUES ('47','28','7','1');
INSERT INTO `skill_buff` VALUES ('48','29','61','1');
INSERT INTO `skill_buff` VALUES ('49','135','46','1');
INSERT INTO `skill_buff` VALUES ('50','143','2','0.2');
INSERT INTO `skill_buff` VALUES ('51','136','62','1');
INSERT INTO `skill_buff` VALUES ('52','144','25','1');

-- ----------------------------
-- Data for potion
-- ----------------------------

INSERT INTO `potion` VALUES ('1','初级HP药剂','HP','50');
INSERT INTO `potion` VALUES ('2','中级HP药剂','HP','100');
INSERT INTO `potion` VALUES ('3','高级HP药剂','HP','150');
INSERT INTO `potion` VALUES ('4','初级PP药剂','PP','5');
INSERT INTO `potion` VALUES ('5','中级PP药剂','PP','10');
INSERT INTO `potion` VALUES ('6','高级PP药剂','PP','15');

-- ----------------------------
-- Data for ball
-- ----------------------------

INSERT INTO `ball` VALUES ('1','初级精灵球','1','0.5');
INSERT INTO `ball` VALUES ('2','中级精灵球','2','0.6');
INSERT INTO `ball` VALUES ('3','高级精灵球','3','0.7');
INSERT INTO `ball` VALUES ('4','大师精灵球','4','0.9');