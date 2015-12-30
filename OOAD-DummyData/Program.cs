using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOAD_DummyData
{

    class Program
    {
                        
        static void Main(string[] args)
        {
            DummyData dd = new DummyData();
          
            dd.PrintTitle("Select");
            dd.select();

            int sim_price_count = 50;
            int sim_price_minimum = 30;
            int sim_price_maximum = 700;
            dd.PrintTitle("Sim Price");
            dd.sim_price(sim_price_count, sim_price_minimum, sim_price_maximum);

            
            double mobile_numbers_start = 03333902985;
            int mobile_numbers_count = 4000;
            dd.PrintTitle("Mobile Numbers");
            dd.mobile_numbers(mobile_numbers_start, mobile_numbers_count);


            int customer_count = 3000;
            int customer_minimum = 0;
            int customer_maximum = 21900;
            dd.PrintTitle("Customer");
            dd.customer(customer_count, customer_minimum, customer_maximum, mobile_numbers_start, sim_price_count);

            int call_unit_price_count = 50;
            double call_unit_price_minimum = 0.50; 
            double call_unit_price_maximum = 20.25;
            dd.PrintTitle("Call Unit Price");
            dd.call_unit_price(call_unit_price_count,call_unit_price_minimum,call_unit_price_maximum);

            int call_record_count = 15000;
             double call_duration_min = 0.10;
             double call_duration_max = 280.65;
            dd.PrintTitle("Call Record");
            dd.call_record(call_record_count, customer_count, call_duration_min, call_duration_max,call_unit_price_count);

            int package_count = 50;
            dd.PrintTitle("Package");
            dd.package(package_count);

            dd.PrintTitle("Discount Offers");
            dd.discount_offers(package_count);

            int customer_package_count = 4000;
            dd.PrintTitle("Customer Package");
            dd.customer_package(customer_package_count, customer_count, package_count);

            dd.PrintTitle("Department");
            dd.department();

            int memo_count = 100;
            dd.PrintTitle("Memo");
            dd.memo(memo_count);

            int budget_count = 300;
            dd.PrintTitle("Budget");
            dd.budget(budget_count);

            int marketing_campaign_count = 300;
            dd.PrintTitle("Marketing Campaign");
            dd.marketing_campaign(marketing_campaign_count);

            dd.PrintTitle("Employee");
            dd.employee();

            int customer_complaint_count = 1000;
            dd.PrintTitle("Customer Complaint");
            dd.customer_complaint(customer_complaint_count, customer_count);

            int customer_feedback_count = 1500;
            dd.PrintTitle("Customer Feedback");
            dd.customer_feedback(customer_feedback_count, customer_count);
           
        }
    }
    public class DummyData
    {
        StreamWriter writer;
        StreamReader reader;
        Random random = new Random();
        DateTime start = new DateTime(1980, 1, 1);
        RandomDateTime rdt = new RandomDateTime();
       public DummyData() {
            writer = new StreamWriter("CMS-OOAD-DummyData.txt");
            reader = new StreamReader("namelist.txt");
        }

        public void PrintTitle(String name)
        {
            writer.WriteLine("\n");
            String line = "--++++++++++++++++++++++++++++++++++++++++++";
            writer.WriteLine(line);
            writer.WriteLine("--\t\t"+name);
            writer.WriteLine(line);
            writer.WriteLine("\n");
            writer.WriteLine("\n");
            writer.Flush();

        }

        public void select()
        {
            String[] tableArray = { "sim_price", "customer", "mobile_numbers", "sim_sale_invoice", "registered_number", "call_unit_price", "call_record", "package", "discount_offers", "customer_package", "department", "memo", "budget", "marketing_campaign", "employee", "customer_complaint", "customer_feedback" };

            foreach (var table in tableArray)
            {
                writer.WriteLine("SELECT * FROM {0}", table);
            }
            writer.Flush();

        }
        public void sim_price(int sim_price_count, int minimum, int maximum)
        {
            for (int i = 0; i < sim_price_count; i++)
            {
                writer.WriteLine("INSERT INTO sim_price(sp_sim_price) VALUES(" + GetRandomNumberInt(minimum, maximum) + ")");
            }
           
            writer.Flush();
        }

        public void customer(int customer_count, int customer_minimum, int customer_maximum, double mobile_numbers_start, int sim_price_count)
        {
            
            List<string> list = new List<string>();
            double mns = mobile_numbers_start;
            double mns2 = mobile_numbers_start;
            double mns3 = mobile_numbers_start;
            double mns4 = mobile_numbers_start;
            string line;
            string password = "123";

            while ((line = reader.ReadLine()) != null)
            {
                list.Add(line); // Add to list.
            }

            String[] nameArray = list.ToArray();

            for (int i = 0; i < customer_count; i++)
            {
                String name = nameArray[GetRandomNumberInt(customer_minimum, customer_maximum)];
                String[] teamArr = name.Split(' ');
                String fisrtName = teamArr[0].Trim().ToLower();
                String lastName = teamArr[1].Trim().ToLower();
                String userName = fisrtName + lastName;

                
                writer.WriteLine("INSERT INTO customer(customer_name,customer_nic,customer_email,customer_dob,userName,userPassword,customer_joinDate) VALUES('" + name + "','" + GetRandomNumberNIC() + "','" + userName + "@gmail.com','" + rdt.Next() + "','" + userName + "','" + password + "','" + rdt.Next() + "')");
                
               
                mns++;
            }
            PrintTitle("Registered Number");           
            for (int i = 0; i < customer_count; i++)
            {
                int cid = GetRandomNumberInt(1, customer_count);
                writer.WriteLine("INSERT INTO registered_number(customerID,mobile_number) VALUES(" + cid + ",'" + mns2 + "')");
                mns2++;
            }
            for (int i = 0; i < customer_count; i++)
            {
                writer.WriteLine("UPDATE mobile_numbers SET mn_available=0 WHERE mn_number='" + mns3 + "'");
                mns3++;
            }
            PrintTitle("Sim Sale Invoice");
            for (int i = 0; i < customer_count; i++)
            {
                int cid = GetRandomNumberInt(1, customer_count);
                writer.WriteLine("INSERT INTO sim_sale_invoice(ssi_mobileNumber,ssi_cid,ssi_sim_price,ssi_DateTime) VALUES('" + mns4 + "'," + cid + "," + GetRandomNumberInt(1, sim_price_count) + ",'" + rdt.Next() + "')");
                mns4++;
            }
          
            writer.Flush();
        }
        public void mobile_numbers(double mobile_numbers_start,int mobile_numbers_count)
        {
            double mns = mobile_numbers_start;
            for (int i = 0; i < mobile_numbers_count; i++)
            {
                writer.WriteLine("INSERT INTO mobile_numbers(mn_number) VALUES('" + mns + "')");
                mns++;
            }
            writer.Flush();
        }

        public void call_unit_price(int call_unit_price_count, double call_unit_price_minimum, double call_unit_price_maximum)
        {
            for (int i = 0; i < call_unit_price_count; i++)
            {
                writer.WriteLine("INSERT INTO call_unit_price(cup_unit_charges,cup_DateTime) VALUES(" + GetRandomNumberDouble(call_unit_price_minimum, call_unit_price_maximum) + ",'" + rdt.Next() + "')");
            }
            writer.Flush();
        }
        public void call_record(int call_record_count, int customer_count, double call_duration_min, double call_duration_max, int call_unit_price_count)
        {
            for (int i = 0; i < call_record_count; i++)
            {
                writer.WriteLine("INSERT INTO call_record(call_from,call_to,call_duration,call_unit_charges) VALUES(" + GetRandomNumberInt(1, customer_count) + "," + GetRandomNumberInt(1, customer_count) + "," + GetRandomNumberDouble(call_duration_min, call_duration_max) + "," + GetRandomNumberInt(1, call_unit_price_count) + ")");
            }
           
            writer.Flush();
        }
        public void package(int package_count)
        {
            for (int i = 0; i < package_count; i++)
            {
                int x = i;
                ++x;
                writer.WriteLine("INSERT INTO package(p_title,p_desc) VALUES('Package " + x + "','Package " + x + " Description')");
            }
           
            writer.Flush();
        }
        public void discount_offers(int package_count)
        {
            for (int i = 0; i < package_count; i++)
            {
                int x = i;
                ++x;
                writer.WriteLine("INSERT INTO discount_offers(do_title,do_desc,do_packageID,do_percentage_off) VALUES('Discount Offer " + x + "','Discount Offer " + x + " Description'," + GetRandomNumberInt(1, package_count) + ","+GetRandomNumberInt(2, 50)+")");
            }
            writer.Flush();
        }
        public void customer_package(int customer_package_count,int customer_count,int package_count)
        {
            for (int i = 0; i < customer_package_count; i++)
            {
                writer.WriteLine("INSERT INTO customer_package(cp_cid,cp_pid) VALUES(" + GetRandomNumberInt(1, customer_count) + "," + GetRandomNumberInt(1, package_count) + ")");
            }
          
            writer.Flush();
        }
        public void department()
        {
            string[] departmentArray = { "Sales", "Marketing", "Customer Support", "Human Resources" };

            foreach (var department in departmentArray)
            {
                writer.WriteLine("INSERT INTO department(d_name) VALUES('" + department + "')");
            }   
            writer.Flush();
        }
        public void memo(int memo_count)
        {
            for (int i = 0; i < memo_count; i++)
            {
                int x = i;
                ++x;
                writer.WriteLine("INSERT INTO memo(m_title,m_desc,m_department) VALUES('Memo " + x + "','Memo " + x + " Description'," + GetRandomNumberInt(1, 4) + ")");
            }
            
            writer.Flush();
        }
        public void budget(int budget_count)
        {
            for (int i = 0; i < budget_count; i++)
            {
                writer.WriteLine("INSERT INTO budget(b_month,b_value) VALUES('" + rdt.Next() + "'," + GetRandomNumberInt(50000, 50000000) + ")");
            }
            
            writer.Flush();
        }
        public void marketing_campaign(int marketing_campaign_count)
        {
            for (int i = 0; i < marketing_campaign_count; i++)
            {
                int x = i;
                ++x;
                writer.WriteLine("INSERT INTO marketing_campaign(mc_title,mc_desc,mc_approval_status,mc_budget,mc_department,mc_DATETIME) VALUES('Marketing Campaign " + x + "','Marketing Campaign " + x + " Description',0," + GetRandomNumberInt(5000, 500000) + "," + GetRandomNumberInt(1, 4) + ",'" + rdt.Next() + "')");
            }
            int approve = marketing_campaign_count / 2;
            for (int i = 0; i < approve; i++)
            {
                writer.WriteLine("UPDATE marketing_campaign SET mc_approval_status=1 WHERE mcustomer_id=" + GetRandomNumberInt(1, marketing_campaign_count) + "");
            }

            writer.Flush();
        }
        public void employee()
        {
            writer.WriteLine("INSERT INTO employee(e_name,e_email,e_contact,userName,userPassword,userRole,e_dID) VALUES('Ali','ali@gmail.com','0333333331','e1','123',2,4)");
            writer.WriteLine("INSERT INTO employee(e_name,e_email,e_contact,userName,userPassword,userRole,e_dID) VALUES('Khan','khan@gmail.com','0333333332','e2','123',3,2)");
            writer.WriteLine("INSERT INTO employee(e_name,e_email,e_contact,userName,userPassword,userRole,e_dID) VALUES('Hassan','hassan@gmail.com','0333333333','e3','123',4,3)");
            writer.WriteLine("INSERT INTO employee(e_name,e_email,e_contact,userName,userPassword,userRole,e_dID) VALUES('Saad','saad@gmail.com','0333333334','e4','123',5,1)");

            writer.Flush();
        }

        public void customer_complaint(int customer_complaint_count, int customer_count)
        {
            for (int i = 0; i < customer_complaint_count; i++)
            {
                int x = i;
                ++x;
                writer.WriteLine("INSERT INTO customer_complaint(cc_title,cc_desc,cc_department,cc_DATETIME,cc_cid) VALUES('Complaint " + x + "','Complaint " + x + " Description'," + GetRandomNumberInt(1, 4) + ",'" + rdt.Next() + "'," + GetRandomNumberInt(1, customer_count) + ")");
            }
           
            writer.Flush();
        }
        public void customer_feedback(int customer_feedback_count, int customer_count)
        {
            for (int i = 0; i < customer_feedback_count; i++)
            {
                int x = i;
                ++x;
                writer.WriteLine("INSERT INTO customer_feedback(cfb_title,cfb_desc,cfb_DATETIME,cfb_cid) VALUES('Customer Feedback " + x + "','Customer Feedback " + x + " Description','" + rdt.Next() + "'," + GetRandomNumberInt(1, customer_count) + ")");
            }
           
            writer.Flush();
        }

        public double GetRandomNumberDouble(double minimum, double maximum)
        {

            return random.NextDouble() * (maximum - minimum) + minimum; ;
        }
        public int GetRandomNumberInt(int minimum, int maximum)
        {
            return random.Next(minimum,maximum);
        }

        public string GetRandomNumberNIC()
        {
            string nic = "" + random.Next(0, 99999) + "-" + random.Next(0, 9999999) + "-" + random.Next(0,9);
            return nic;
        }
    }
    class RandomDateTime
    {
        DateTime start;
        Random gen = new Random();
        int range;

        public RandomDateTime()
        {
            start = new DateTime(1980, 1, 1);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }
}
