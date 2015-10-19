using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 产品搜索模板
    /// </summary>
    public class SearchModel
    {
        public static List<Model.SearchModel> GetList(SearchModelEnum sme)
        {
            switch (sme)
            {
                case SearchModelEnum.药理一级:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = -1, name = "西药", tid = 1 });
                        li.Add(new Model.SearchModel { id = -2, name = "中成药", tid = 583 });
                        //li.Add(new Model.SearchModel { id = -3, name = "医疗器械", tid = 2968 });
                        return li;
                    }
                case SearchModelEnum.药理二级:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 101, name = "心血管系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 145, name = "消化系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 177, name = "呼吸系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 192, name = "血液系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 217, name = "神经系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 239, name = "精神障碍", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 257, name = "代谢及内分泌", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 296, name = "肾泌尿系统用", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 307, name = "运动系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 310, name = "麻醉用药及麻", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 319, name = "男性生殖系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 323, name = "女性生殖系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 330, name = "肿瘤", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 341, name = "免疫系统", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 353, name = "皮肤及皮下", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 362, name = "耳鼻喉科", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 363, name = "眼科", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 368, name = "口腔科", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 370, name = "抗生素", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 395, name = "合成抗", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 401, name = "抗真菌药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 410, name = "抗病毒药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 419, name = "抗分枝杆菌药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 422, name = "抗寄生虫药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 438, name = "消毒防腐药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 445, name = "天然来源抗感染", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 464, name = "镇痛药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 477, name = "抗变态反应药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 481, name = "其它药物", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 488, name = "其它专科", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 499, name = "生物制品", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 516, name = "制药用品及医疗用具", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 781, name = "酶类及其它生化药", ParentID = -1 });
                        li.Add(new Model.SearchModel { id = 446, name = "药电解质酸碱平衡及营", ParentID = -1 });


                        li.Add(new Model.SearchModel { id = 584, name = "解表剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 619, name = "祛暑剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 612, name = "清热剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 658, name = "理血剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 627, name = "补益剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 653, name = "理气剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 673, name = "祛湿剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 680, name = "祛痰剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 690, name = "止咳平喘剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 702, name = "治疮疡剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 693, name = "消食剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 593, name = "泻下剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 600, name = "和解剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 607, name = "温里剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 638, name = "固涩剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 645, name = "安神剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 649, name = "开窍剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 668, name = "治风剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 698, name = "治燥剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 709, name = "驱虫止痒剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 795, name = "制酸止痛解痉剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 796, name = "局部用止痛剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 797, name = "清目剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = 710, name = "其他", ParentID = -2 });

                        //li.Add(new Model.SearchModel { id = 2969, name = "一类", ParentID = -3 });
                        //li.Add(new Model.SearchModel { id = 2970, name = "二类", ParentID = -3 });
                        //li.Add(new Model.SearchModel { id = 2971, name = "三类", ParentID = -3 });
                        return li;
                    }
                case SearchModelEnum.剂型一级:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = -3, name = "口服常释", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -4, name = "注射剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -5, name = "其他热门", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -6, name = "缓释控释", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -7, name = "口服液体", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -8, name = "颗粒剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -9, name = "散/凝胶剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -10, name = "软膏剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -11, name = "贴剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -12, name = "外用液体", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -13, name = "涂/栓剂", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -14, name = "五官科", ParentID = -2 });
                        li.Add(new Model.SearchModel { id = -15, name = "吸入剂", ParentID = -2 });
                        return li;
                    }

                case SearchModelEnum.剂型二级:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 1, name = "片", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 2, name = "肠溶片", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 3, name = "浸膏片", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 4, name = "分散片", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 5, name = "胶囊", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 6, name = "软胶囊", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 7, name = "胶丸", ParentID = -3 });
                        li.Add(new Model.SearchModel { id = 8, name = "肠溶胶囊", ParentID = -3 });


                        li.Add(new Model.SearchModel { id = 9, name = "粉针（注射用）", ParentID = -4, condition = " and (DrugsBase_DrugName like ('%注射用%') or DrugsBase_DrugName like ('%粉针%'))" });
                        li.Add(new Model.SearchModel { id = 10, name = "注射液", ParentID = -4 });
                        li.Add(new Model.SearchModel { id = 11, name = "氯化钠注射液", ParentID = -4 });
                        li.Add(new Model.SearchModel { id = 12, name = "葡萄糖注射液", ParentID = -4 });


                        li.Add(new Model.SearchModel { id = 13, name = "泡腾片", ParentID = -5 });
                        li.Add(new Model.SearchModel { id = 14, name = "咀嚼片", ParentID = -5 });
                        li.Add(new Model.SearchModel { id = 15, name = "崩解片", ParentID = -5 });
                        li.Add(new Model.SearchModel { id = 16, name = "肠溶", ParentID = -5 });
                        li.Add(new Model.SearchModel { id = 17, name = "含片", ParentID = -5 });
                        li.Add(new Model.SearchModel { id = 18, name = "滴丸", ParentID = -5 });


                        li.Add(new Model.SearchModel { id = 19, name = "缓释片", ParentID = -6 });
                        li.Add(new Model.SearchModel { id = 20, name = "控释片", ParentID = -6 });
                        li.Add(new Model.SearchModel { id = 21, name = "缓释胶囊", ParentID = -6 });
                        li.Add(new Model.SearchModel { id = 22, name = "控释胶囊", ParentID = -6 });
                        li.Add(new Model.SearchModel { id = 23, name = "缓释植入剂", ParentID = -6 });


                        li.Add(new Model.SearchModel { id = 24, name = "口服液", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 25, name = "口服溶液", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 26, name = "混悬液", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 27, name = "干混悬剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 28, name = "口服乳剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 29, name = "胶浆", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 30, name = "乳液", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 31, name = "乳剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 32, name = "合剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 33, name = "酊", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 34, name = "滴剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 35, name = "混悬滴剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 36, name = "糖浆", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 37, name = "糖浆剂", ParentID = -7 });
                        li.Add(new Model.SearchModel { id = 38, name = "干糖浆", ParentID = -7 });


                        li.Add(new Model.SearchModel { id = 39, name = "颗粒（剂）", ParentID = -8, condition = " and  DrugsBase_DrugName like ('%颗粒%')" });
                        li.Add(new Model.SearchModel { id = 40, name = "冲剂", ParentID = -8 });
                        li.Add(new Model.SearchModel { id = 41, name = "肠溶颗粒", ParentID = -8 });


                        li.Add(new Model.SearchModel { id = 42, name = "散（剂）", ParentID = -9, condition = " and  DrugsBase_DrugName like ('%散%')" });
                        li.Add(new Model.SearchModel { id = 43, name = "粉（剂）", ParentID = -9, condition = " and  DrugsBase_DrugName like ('%粉%')" });
                        li.Add(new Model.SearchModel { id = 44, name = "撒粉", ParentID = -9 });
                        li.Add(new Model.SearchModel { id = 45, name = "乳胶剂", ParentID = -9 });
                        li.Add(new Model.SearchModel { id = 46, name = "凝胶（剂）", ParentID = -9, condition = " and  DrugsBase_DrugName like ('%凝胶%')" });


                        li.Add(new Model.SearchModel { id = 47, name = "软膏", ParentID = -10 });
                        li.Add(new Model.SearchModel { id = 48, name = "乳膏", ParentID = -10 });
                        li.Add(new Model.SearchModel { id = 49, name = "霜", ParentID = -10 });
                        li.Add(new Model.SearchModel { id = 50, name = "糊剂", ParentID = -10 });
                        li.Add(new Model.SearchModel { id = 51, name = "膏药", ParentID = -10 });
                        li.Add(new Model.SearchModel { id = 52, name = "膏", ParentID = -10 });


                        li.Add(new Model.SearchModel { id = 53, name = "贴", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 54, name = "贴片", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 55, name = "贴膏", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 56, name = "贴剂", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 57, name = "贴膜", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 58, name = "透皮贴片", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 59, name = "透皮贴剂", ParentID = -11 });
                        li.Add(new Model.SearchModel { id = 60, name = "膜", ParentID = -11 });


                        li.Add(new Model.SearchModel { id = 61, name = "外用溶液（剂）", ParentID = -12, condition = " and  DrugsBase_DrugName like ('%外用溶液%')" });
                        li.Add(new Model.SearchModel { id = 62, name = "洗剂", ParentID = -12 });
                        li.Add(new Model.SearchModel { id = 63, name = "含涑液", ParentID = -12 });
                        li.Add(new Model.SearchModel { id = 64, name = "胶浆", ParentID = -12 });
                        li.Add(new Model.SearchModel { id = 65, name = "搽剂", ParentID = -12 });
                        li.Add(new Model.SearchModel { id = 66, name = "酊", ParentID = -12 });
                        li.Add(new Model.SearchModel { id = 67, name = "油", ParentID = -12 });


                        li.Add(new Model.SearchModel { id = 68, name = "涂剂", ParentID = -13 });
                        li.Add(new Model.SearchModel { id = 69, name = "涂膜剂", ParentID = -13 });
                        li.Add(new Model.SearchModel { id = 70, name = "栓（剂）", ParentID = -13, condition = " and  DrugsBase_DrugName like ('%栓%')" });
                        li.Add(new Model.SearchModel { id = 71, name = "阴道栓", ParentID = -13 });


                        li.Add(new Model.SearchModel { id = 72, name = "滴眼液", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 73, name = "滴眼剂", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 74, name = "滴耳液", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 75, name = "滴耳剂", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 76, name = "滴耳油", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 77, name = "滴鼻液", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 78, name = "滴鼻剂", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 79, name = "滴鼻油", ParentID = -14 });
                        li.Add(new Model.SearchModel { id = 80, name = "滴鼻用", ParentID = -14 });


                        li.Add(new Model.SearchModel { id = 81, name = "喷剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 82, name = "喷鼻剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 83, name = "气雾剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 84, name = "喷雾剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 85, name = "雾化溶液", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 86, name = "雾化液", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 87, name = "雾化吸入溶液", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 88, name = "雾化吸入剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 89, name = "吸入用", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 90, name = "吸入剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 91, name = "吸入粉雾剂", ParentID = -15 });
                        li.Add(new Model.SearchModel { id = 92, name = "吸入溶液", ParentID = -15 });


                        return li;
                    }


                case SearchModelEnum.厂家数量:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 1, name = "独家", condition = " and [DrugsBase_ID] in (select [DrugsBase_ID] from [ViewDrugsBase] where Exclusive=1)" });
                        li.Add(new Model.SearchModel { id = 2, name = "2家", condition = " and [DrugsBase_ID] in (select [DrugsBase_ID] from [ViewDrugsBase] where Exclusive=2)" });
                        li.Add(new Model.SearchModel { id = 3, name = "3家", condition = " and [DrugsBase_ID] in (select [DrugsBase_ID] from [ViewDrugsBase] where Exclusive=3)" });
                        li.Add(new Model.SearchModel { id = 4, name = "4家", condition = " and [DrugsBase_ID] in (select [DrugsBase_ID] from [ViewDrugsBase] where Exclusive=4)" });
                        li.Add(new Model.SearchModel { id = 5, name = "5家以上", condition = " and [DrugsBase_ID] in (select [DrugsBase_ID] from [ViewDrugsBase] where Exclusive>4)" });
                        return li;
                    }
                case SearchModelEnum.价格区间:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 1, name = "0-10元", condition = " and (Price_01<10 or Price_02<10)" });
                        li.Add(new Model.SearchModel { id = 2, name = "10-20元", condition = " and ((Price_01>10 and Price_01<21) or (Price_02>10 and Price_02<21))" });
                        li.Add(new Model.SearchModel { id = 3, name = "20-50元", condition = " and ((Price_01>20 and Price_01<50) or (Price_02>20 and Price_02<50))" });
                        li.Add(new Model.SearchModel { id = 4, name = "50-100元", condition = " and ((Price_01>50 and Price_01<100) or (Price_02>50 and Price_02<100))" });
                        li.Add(new Model.SearchModel { id = 5, name = "100元以上", condition = " and (Price_01>100 or Price_02>100)" });
                        return li;
                    }
                case SearchModelEnum.热门标签:
                    {
                        string sql = " and product_id  in (SELECT product_id from dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN (SELECT id FROM dbo.Tag_PharmAttribute WHERE tag_id={0}))";
                        string sql2 = " and product_id in (SELECT product_id from dbo.Tag_Product WHERE tag_id={0})";
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        //li.Add(new Model.SearchModel { id = 20, name = "东昌特供", condition = string.Format(sql, 92) });
                        //li.Add(new Model.SearchModel { id = 19, name = "东昌工业", condition = string.Format(sql2, 89) });
                        li.Add(new Model.SearchModel { id = 6, name = "基药", condition = string.Format(sql2, 66) });
                        li.Add(new Model.SearchModel { id = 14, name = "OTC", condition = " and product_id IN(SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN(SELECT id FROM Tag_PharmAttribute WHERE tag_id=71))" });
                        li.Add(new Model.SearchModel { id = 17, name = "注射剂", condition = " and DrugsBase_DrugName like('%注射%')" });
                        //li.Add(new Model.SearchModel { id = 1, name = "独家", condition = " and [DrugsBase_ID] in (select [DrugsBase_ID] from [ViewDrugsBase] where Exclusive=1)" });
                        // li.Add(new Model.SearchModel { id = 2, name = "医保", condition = " and product_id in (SELECT  DrugsBase_ID from dbo.DrugsBase_HealthInsuranceType WHERE DrugsBase_HealthInsuranceType>0)" });
                        //li.Add(new Model.SearchModel { id = 3, name = "中标", condition = string.Format(sql2, 2) });
                        //li.Add(new Model.SearchModel { id = 4, name = "原研", condition = string.Format(sql2, 10) });
                        //li.Add(new Model.SearchModel { id = 5, name = "控销", condition = " and price_03>0 " });
                        li.Add(new Model.SearchModel { id = 11, name = "进口", condition = " and DrugsBase_ID IN (SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN(SELECT id FROM dbo.Tag_PharmAttribute WHERE tag_id=88))" });
                        //li.Add(new Model.SearchModel { id = 5, name = "临床", condition = " and 1<>1" });
                        //li.Add(new Model.SearchModel { id = 7, name = "中药保护", condition = " and 1<>1" });//DrugsBase_MedicineType=1" });
                        //li.Add(new Model.SearchModel { id = 8, name = "新药", condition = " and tag_ids like('%,24,%')" });//and DrugsBase_bNewDrugs=1 " });
                        //li.Add(new Model.SearchModel { id = 9, name = "专利", condition = " and 1<>1" });
                        //li.Add(new Model.SearchModel { id = 10, name = "胃肠", condition = " and 1<>1" });                        
                        //li.Add(new Model.SearchModel { id = 12, name = "傣药", condition = " and 1<>1" });
                        //li.Add(new Model.SearchModel { id = 13, name = "藏药", condition = " and 1<>1" });
                        //li.Add(new Model.SearchModel { id = 15, name = "抗生素", condition = " and 1<>1" });
                        //li.Add(new Model.SearchModel { id = 16, name = "维生素", condition = " and DrugsBase_DrugName like('%维生素%')" });
                        //li.Add(new Model.SearchModel { id = 18, name = "市场保护", condition = " and 1<>1" });
                        return li;
                    }
                case SearchModelEnum.品牌厂家:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 1, name = "济南利民", condition = " and DrugsBase_Manufacturer like('%济南利民%')" });
                        li.Add(new Model.SearchModel { id = 2, name = "哈尔滨一洲", condition = " and DrugsBase_Manufacturer like('%哈尔滨一洲%')" });
                        li.Add(new Model.SearchModel { id = 3, name = "山西普德", condition = "  and DrugsBase_Manufacturer like('%山西普德%')" });
                        li.Add(new Model.SearchModel { id = 4, name = "河南灵佑", condition = " and DrugsBase_Manufacturer like('%河南灵佑%')" });
                        li.Add(new Model.SearchModel { id = 5, name = "大连天宇", condition = " and DrugsBase_Manufacturer like('%大连天宇%')" });
                        li.Add(new Model.SearchModel { id = 6, name = "悦康药业", condition = " and DrugsBase_Manufacturer like('%悦康药业%')" });
                        li.Add(new Model.SearchModel { id = 7, name = "哈尔滨三联", condition = " and DrugsBase_Manufacturer like('%哈尔滨三联%')" });
                        li.Add(new Model.SearchModel { id = 8, name = "湖南德康", condition = " and DrugsBase_Manufacturer like('%湖南德康%')" });
                        li.Add(new Model.SearchModel { id = 9, name = "雷允上药业", condition = " and DrugsBase_Manufacturer like('%雷允上药业%')" });
                        li.Add(new Model.SearchModel { id = 10, name = "广东嘉博", condition = " and DrugsBase_Manufacturer like('%广东嘉博%')" });
                        li.Add(new Model.SearchModel { id = 11, name = "山东淄博", condition = " and DrugsBase_Manufacturer like('%山东淄博%')" });
                        li.Add(new Model.SearchModel { id = 12, name = "江西民济", condition = " and DrugsBase_Manufacturer like('%江西民济%')" });
                        li.Add(new Model.SearchModel { id = 13, name = "福和华星", condition = " and DrugsBase_Manufacturer like('%福和华星%')" });
                        li.Add(new Model.SearchModel { id = 14, name = "烟台天正", condition = " and DrugsBase_Manufacturer like('%烟台天正%')" });
                        li.Add(new Model.SearchModel { id = 15, name = "吉林紫鑫", condition = " and DrugsBase_Manufacturer like('%吉林紫鑫%')" });
                        li.Add(new Model.SearchModel { id = 16, name = "四川健能", condition = " and DrugsBase_Manufacturer like('%四川健能%')" });
                        li.Add(new Model.SearchModel { id = 17, name = "江西百神", condition = " and DrugsBase_Manufacturer like('%江西百神%')" });
                        li.Add(new Model.SearchModel { id = 18, name = "成都天台山", condition = " and DrugsBase_Manufacturer like('%成都天台山%')" });
                        li.Add(new Model.SearchModel { id = 19, name = "西南药业", condition = " and DrugsBase_Manufacturer like('%西南药业%')" });
                        li.Add(new Model.SearchModel { id = 20, name = "重庆巨琪", condition = " and DrugsBase_Manufacturer like('%重庆巨琪%')" });
                        li.Add(new Model.SearchModel { id = 21, name = "江苏晨牌", condition = " and DrugsBase_Manufacturer like('%江苏晨牌%')" });
                        li.Add(new Model.SearchModel { id = 22, name = "深圳信立泰", condition = " and DrugsBase_Manufacturer like('%深圳信立泰%')" });
                        li.Add(new Model.SearchModel { id = 23, name = "四川志远广和", condition = " and DrugsBase_Manufacturer like('%四川志远广和%')" });
                        li.Add(new Model.SearchModel { id = 24, name = "重庆药友", condition = " and DrugsBase_Manufacturer like('%重庆药友%')" });
                        li.Add(new Model.SearchModel { id = 25, name = "天圣制药", condition = " and DrugsBase_Manufacturer like('%天圣制药%')" });
                        li.Add(new Model.SearchModel { id = 26, name = "潍坊中狮", condition = " and DrugsBase_Manufacturer like('%潍坊中狮%')" });
                        li.Add(new Model.SearchModel { id = 27, name = "深圳致君", condition = " and DrugsBase_Manufacturer like('%深圳致君%')" });
                        li.Add(new Model.SearchModel { id = 28, name = "华北制药", condition = " and DrugsBase_Manufacturer like('%华北制药%')" });
                        li.Add(new Model.SearchModel { id = 29, name = "江西品信", condition = " and DrugsBase_Manufacturer like('%江西品信%')" });
                        li.Add(new Model.SearchModel { id = 30, name = "四川升和", condition = " and DrugsBase_Manufacturer like('%四川升和%')" });
                        li.Add(new Model.SearchModel { id = 31, name = "哈尔滨圣泰", condition = " and DrugsBase_Manufacturer like('%哈尔滨圣泰%')" });
                        li.Add(new Model.SearchModel { id = 32, name = "安徽环球", condition = " and DrugsBase_Manufacturer like('%安徽环球%')" });
                        li.Add(new Model.SearchModel { id = 33, name = "鲁抗医药", condition = " and DrugsBase_Manufacturer like('%鲁抗医药%')" });
                        li.Add(new Model.SearchModel { id = 34, name = "普正制药", condition = " and DrugsBase_Manufacturer like('%普正制药%')" });
                        li.Add(new Model.SearchModel { id = 35, name = "成都迪康", condition = " and DrugsBase_Manufacturer like('%成都迪康%')" });
                        li.Add(new Model.SearchModel { id = 36, name = "四川制药制剂", condition = " and DrugsBase_Manufacturer like('%四川制药制剂%')" });
                        li.Add(new Model.SearchModel { id = 37, name = "京都念慈菴", condition = " and DrugsBase_Manufacturer like('%京都念慈菴%')" });
                        li.Add(new Model.SearchModel { id = 38, name = "广西金嗓子", condition = " and DrugsBase_Manufacturer like('%广西金嗓子%')" });
                        li.Add(new Model.SearchModel { id = 39, name = "千金药业", condition = " and DrugsBase_Manufacturer like('%千金药业%')" });
                        li.Add(new Model.SearchModel { id = 40, name = "香港联邦制药", condition = " and DrugsBase_Manufacturer like('%香港联邦制药%')" });
                        li.Add(new Model.SearchModel { id = 41, name = "兆科药业", condition = " and DrugsBase_Manufacturer like('%兆科药业%')" });
                        li.Add(new Model.SearchModel { id = 42, name = "三九药业", condition = " and DrugsBase_Manufacturer like('%三九药业%')" });
                        li.Add(new Model.SearchModel { id = 43, name = "哈药集团", condition = " and DrugsBase_Manufacturer like('%哈药集团%')" });
                        li.Add(new Model.SearchModel { id = 44, name = "云南白药", condition = " and DrugsBase_Manufacturer like('%云南白药%')" });
                        li.Add(new Model.SearchModel { id = 45, name = "北京同仁堂", condition = " and DrugsBase_Manufacturer like('%北京同仁堂%')" });
                        li.Add(new Model.SearchModel { id = 46, name = "双鹤药业", condition = " and DrugsBase_Manufacturer like('%双鹤药业%')" });
                        return li;
                    }
                case SearchModelEnum.高毛利:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 1, name = "约70%", condition = "" });
                        li.Add(new Model.SearchModel { id = 2, name = "约60%", condition = "" });
                        li.Add(new Model.SearchModel { id = 3, name = "约50%", condition = "" });
                        li.Add(new Model.SearchModel { id = 4, name = "约40%", condition = "" });
                        li.Add(new Model.SearchModel { id = 5, name = "约30%", condition = "" });
                        return li;
                    }
                
                case SearchModelEnum.中药饮片:
                    {
                        string sql = " and product_id in (select DrugsBase_ID from [DrugsBase_PharmMediNameLink] where [Pharm_ID] in (select [Pharm_ID] from [DrugsBase_Pharm] where [Pharm_ID_Path] like '%\\{0}%'))";
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        li.Add(new Model.SearchModel { id = 2974, name = "常规饮片", condition = string.Format(sql, 2974) });
                        li.Add(new Model.SearchModel { id = 2985, name = "精品饮片", condition = string.Format(sql, 2985) });
                        li.Add(new Model.SearchModel { id = 2990, name = "特殊饮片", condition = string.Format(sql, 2990) });
                        li.Add(new Model.SearchModel { id = 2991, name = "药食同源", condition = string.Format(sql, 2991) });
                        li.Add(new Model.SearchModel { id = 2995, name = "中药相关", condition = string.Format(sql, 2995) });
                        return li;
                    }
                case SearchModelEnum.进口药品:
                    {
                        List<Model.SearchModel> li = new List<Model.SearchModel>();
                        SOSOshop.BLL.Category.Menu bll = new Category.Menu();
                        var dt = bll.GetList(Category.MenuEnum.进口药品, 100);
                        foreach (var item in dt)
                        {
                            li.Add(new Model.SearchModel { id = item.Pharm_ID, name = item.Title, condition = " and DrugsBase_ID in (SELECT product_id FROM Tag_PharmProduct WHERE Tag_PharmAttribute_id=" + item.Pharm_ID + ")" });
                        }
                        return li;
                    }
            }
            return null;
        }
    }

    public enum SearchModelEnum
    {
        药理一级, 药理二级, 剂型一级, 剂型二级, 厂家数量, 价格区间, 热门标签, 品牌厂家, 高毛利, 中药饮片, 进口药品
    }
}
