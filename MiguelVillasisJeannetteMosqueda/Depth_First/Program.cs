using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depth_First
{

    class Program
    {

        static void Main(string[] args)
        {
            List<string> origin_city = new List<string>();
            List<string> destiny_city = new List<string>();
            List<string> all_cities = new List<string>();
            List<string> connected_cities = new List<string>();
            int total_of_cities = 0;
            string cities_a_b_cost = "";
            int[,] final_matrix;
            Node[] all_nodes;
            string depth_result = "";
            string breadth_result = "";
            string bidirectional_result = "";
            string limited_depth_result = "";
            string iterative_depth_result = "";
            string hillclimbing_result = "";
            string bestfirst_result = "";
            string beamsearch_result = "";
            string band_result = "";
            string astar_result = "";
            int root = 0; 
            int destiny = 0;
            int reply = 0;
            int max_depth = 0;
            int k_max = 0;
            /*PLEASE CHANGE BOTH PATHS ACCORDING TO YOUR DOWNLOAD LOCATION*/
            string FileSource = @"D:\Documents\Work\Portfolio\AI_CitiesPathfinding\CitiesPathfinding\Cities.csv";
            string FileCopySource = @"D:\Documents\Work\Portfolio\AI_CitiesPathfinding\CitiesCopy.txt";
            if(FileSource != null)
            {
                System.IO.File.Copy(FileSource, FileCopySource);
            }            

            string[] lines = System.IO.File.ReadAllLines(FileCopySource);



            foreach (string line in lines)
            {
                string helper = line.Split(',')[0];
                string connected_city = line.Split(',')[1];
                int arc_cost = Int32.Parse(line.Split(',')[2]);
                cities_a_b_cost = string.Join(",", helper, connected_city, arc_cost);
                connected_cities.Add(cities_a_b_cost);
                origin_city.Add(helper);
                destiny_city.Add(connected_city);

            }
            var all_cities_helper = origin_city.Union(destiny_city);
            all_cities = all_cities_helper.ToList();
            all_cities.Sort();
            total_of_cities = all_cities.Count();
            
            
            /*Menu*/
            final_matrix = Create_Ad_Matrix(total_of_cities);
            all_nodes = Name_Nodes(all_cities, total_of_cities, FileCopySource);
            Fill_Ad_Matrix(final_matrix, all_nodes, connected_cities, total_of_cities);
            System.IO.File.Delete(FileCopySource);
            do
            {
                do
                {
                    System.Console.Clear();
                    System.Console.WriteLine("Choose your option: ");
                    System.Console.WriteLine("\n \t 1) Show all cities ");
                    System.Console.WriteLine("\t 2) Perform Depth First Search ");
                    System.Console.WriteLine("\t 3) Perform Breadth First Search ");
                    System.Console.WriteLine("\t 4) Perform Bidirectional BF Search ");
                    System.Console.WriteLine("\t 5) Perform Limited Depth Search ");
                    System.Console.WriteLine("\t 6) Perform Iterative Depth Search ");
                    System.Console.WriteLine("\t 7) Perform Hill Climbing Search ");
                    System.Console.WriteLine("\t 8) Perform Best First Search ");
                    System.Console.WriteLine("\t 9) Perform Beam Search ");
                    System.Console.WriteLine("\t 10) Perform Branch and Bound Search ");
                    System.Console.WriteLine("\t 11) Perform A Star Search ");
                    System.Console.WriteLine("\t 12) Exit ");
                    reply = int.Parse(System.Console.ReadLine());

                } while (reply > 12 || reply < 1);

                switch (reply)
                {
                    case 1:
                        foreach (var item in all_nodes)
                        {
                            Console.WriteLine(item.number_of_city + ") " + item.nameof_city);
                        }
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        reply = 0;
                        break;
                    case 2:
                        System.Console.WriteLine("Enter your desired root for depth first, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for depth first, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        depth_result = Return_Depth_First(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The Depth First path is: \n\t" + depth_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 3:
                        System.Console.WriteLine("Enter your desired root for breadth first, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for breadth first, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        breadth_result = Return_Breadth_First(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The Breadth First path is: \n\t" + breadth_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 4:
                        System.Console.WriteLine("Enter your desired root for Bidirectional BPS, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for Bidirectional BPS, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        bidirectional_result = Return_Bidirectional_BFS(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The Breadth First path is: \n\t" + bidirectional_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 5:
                        System.Console.WriteLine("Enter your desired max depth, please: ");
                        max_depth = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired root for limited depth, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for limited depth, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        limited_depth_result = Return_Limited_Depth(final_matrix, total_of_cities, root, destiny, all_nodes, max_depth);
                        System.Console.WriteLine("The Depth First path is: \n\t" + limited_depth_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 6:
                        System.Console.WriteLine("Enter your initial max depth, please: ");
                        max_depth = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired root for limited depth, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for limited depth, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        iterative_depth_result = Return_Iterative_Depth(final_matrix, total_of_cities, root, destiny, all_nodes, max_depth);
                        System.Console.WriteLine("The Depth First path is: \n\t" + iterative_depth_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 7:
                        System.Console.WriteLine("Enter your desired root for Hill Climbing, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for Hill Climbing, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        hillclimbing_result = Return_Hill_Climbing(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The Hill Climbing path is: \n\t" + hillclimbing_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 8:
                        System.Console.WriteLine("Enter your desired root for Best First, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for Best First, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        bestfirst_result = Return_Best_First(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The Best First path is: \n\t" + bestfirst_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 9:
                        System.Console.WriteLine("Enter your Max Best number, please: ");
                        k_max = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired root for Beam Search, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for Beam Search, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        beamsearch_result = Return_Beam_Search(final_matrix, total_of_cities, root, destiny, all_nodes, k_max);
                        System.Console.WriteLine("The Beam Search path is: \n\t" + beamsearch_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 10:
                        System.Console.WriteLine("Enter your desired root for Branch and Bound Search, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for Branch and Bound Search, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        band_result = Return_BandB(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The Branch and Bound Search path is: \n\t" + band_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                    case 11:
                        System.Console.WriteLine("Enter your desired root for A Star Search, please: (0-333)");
                        root = int.Parse(System.Console.ReadLine());
                        System.Console.WriteLine("Enter your desired goal for A Star Search, please: (0-333)");
                        destiny = int.Parse(System.Console.ReadLine());
                        astar_result = Return_AStar(final_matrix, total_of_cities, root, destiny, all_nodes);
                        System.Console.WriteLine("The A Star Search path is: \n\t" + astar_result);
                        System.Console.WriteLine("Press any key to return to main menu: ");
                        Console.ReadKey();
                        break;
                }

            

            } while (reply != 12);
            Console.ReadKey();
        

        }
        /*read through csv and assign notes*/
        public static Node[] Name_Nodes(List<string> All_Cities, int cities_count, string FileSource)
        {
            Node[] all_nodes = new Node[cities_count];
            string[] lines = System.IO.File.ReadAllLines(FileSource);
            string city1_helper = "";
            string city2_helper = "";
            int x1_coordenate = 0;
            int y1_coordenate = 0;
            int x2_coordenate = 0;
            int y2_coordenate = 0;

            for (int i = 0; i < cities_count; i++)
            {
                all_nodes[i].number_of_city = i;
                all_nodes[i].nameof_city = All_Cities[i];
            }
            for (int i = 0; i < all_nodes.Length; i++)
            {
                foreach (string line in lines)
                {
                    city1_helper = line.Split(',')[0];
                    x1_coordenate = Int32.Parse(line.Split(',')[3]);
                    y1_coordenate = Int32.Parse(line.Split(',')[4]);
                    x2_coordenate = Int32.Parse(line.Split(',')[5]);
                    y2_coordenate = Int32.Parse(line.Split(',')[6]);
                    city2_helper = line.Split(',')[1];
                    if (string.Compare(all_nodes[i].nameof_city, city1_helper) == 0 && all_nodes[i].x_coordenates == 0 && all_nodes[i].y_coordenates == 0)
                    {
                        all_nodes[i].x_coordenates = x1_coordenate;
                        all_nodes[i].y_coordenates = y1_coordenate;
                    }
                    if (string.Compare(all_nodes[i].nameof_city, city2_helper) == 0 && all_nodes[i].x_coordenates != 0 && all_nodes[i].y_coordenates != 0)
                    {
                        all_nodes[i].x_coordenates = x2_coordenate;
                        all_nodes[i].y_coordenates = y2_coordenate;
                    }
                }
            }
            return all_nodes;
        }

        /*Create the actual adjacent Matrix*/
        public static int[,] Create_Ad_Matrix(int array_dimension)
        {
            int[,] adjacency_matrix = new int[array_dimension, array_dimension];
            for (int i = 0; i < array_dimension; i++)
            {
                for (int j = 0; j < array_dimension; j++)
                {
                    if(i == j)
                    {
                        adjacency_matrix[i, j] = 1;
                    }
                    else
                    {
                        adjacency_matrix[i, j] = 0;
                    }
                    
                }

            }
            return adjacency_matrix;
        }

        public static void Fill_Ad_Matrix(int[,] Matrix, Node[] city_node, List<string> cities_b, int number_of_cities)
        {
            string origin_city = "";
            string destiny_city = "";
            string node_name = "";
            int node_number = 0;
            int destiny_number = 0;
            int arc_cost = 0;
            
            for (int i = 0; i < cities_b.Count(); i++)
            {
                for (int k = 0; k < number_of_cities; k++)
                        {
                            origin_city = cities_b[i].Split(',')[0];
                            destiny_city = cities_b[i].Split(',')[1];
                            arc_cost = int.Parse(cities_b[i].Split(',')[2]);
                            node_number = city_node[k].number_of_city;
                            node_name = city_node[k].nameof_city;
                            for (int l = 0; l < number_of_cities; l++)
                            {
                                if (string.Compare(destiny_city, city_node[l].nameof_city) == 0)
                                {
                                    destiny_number = city_node[l].number_of_city;
                                    break;
                                }
                            }

                            if (string.Compare(origin_city, node_name) == 0)
                            {

                                Matrix[node_number, destiny_number] = arc_cost;
                                Matrix[destiny_number, node_number] = arc_cost;
                                break;
                            }
                            
                        }
                    
            }
        }

        public static string Return_Depth_First(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string depth_path = "";
            Stack Agenda = new Stack();
            int node_number = 0;
            string name_of_node = "";
            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {

                        if (Matrix[i, j] > 0 && (j > i))
                        {
                            Agenda.Push(j);
                            for (int n = 0; n < matrix_dimension; n++)
                            {
                                if(city_name_node[n].number_of_city == j)
                                {
                                    name_of_node = city_name_node[n].nameof_city;
                                    if (!depth_path.Contains(name_of_node))
                                    {
                                            depth_path = string.Join(",", name_of_node, depth_path);
                                            break;
                                    }
                                }
                            }
                               
                        }
                    
                }
         
                if(Agenda.Count == 0)
                {
                    depth_path = string.Join(",", "This path is incomplete: ", depth_path);
                    break;
                }
                node_number = int.Parse(Agenda.Pop().ToString());
                
                if (node_number == desired_destiny)
                {
                    break;
                }
                    
                i = node_number - 1;
            }
         
            return depth_path;
            
        }

        public static string Return_Breadth_First(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string breadth_path = "";
            List<int> breadth_agenda = new List<int>();
            int node_number = 0;
            string name_of_node = "";
            
            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {
                        if (Matrix[i, j] > 0 && (j > i))
                        {
                            breadth_agenda.Add(j);
                      
                            for (int n = 0; n < matrix_dimension; n++)
                            {
                                if (city_name_node[n].number_of_city == j)
                                {
                                    name_of_node = city_name_node[n].nameof_city;
                                // Imprime camino completo si if se comenta
                                    if (!breadth_path.Contains(name_of_node))
                                    {
                                        breadth_path = string.Join(",", name_of_node, breadth_path);
                                        break;
                                    }
                                }
                            }
                        }
                }
                if (breadth_agenda.Count > 0)
                {
                    node_number = breadth_agenda[0];
                    breadth_agenda.RemoveAt(0);
                }
                else
                {
                    breadth_path = string.Join(",", "This path is incomplete: ", breadth_path);
                    break;
                }
                    
                if (node_number == desired_destiny)
                    break;

                i = node_number - 1;
            }

            return breadth_path;

        }

        public static string Return_Bidirectional_BFS(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string origin_path = "";
            string destiny_path = "";
            string bi_path = "";
            string intersected_cities = "";
            List<int> origin_agenda = new List<int>();
            List<int> destiny_agenda = new List<int>();
            string origin_name_of_node = "";
            string destiny_name_of_node = "";
            bool complete_path = false;

            origin_agenda.Add(desired_root);
            destiny_agenda.Add(desired_destiny);

            while(origin_agenda.Count > 0 && destiny_agenda.Count > 0)
            {
                
                //for (int i = desired_root; i < matrix_dimension; i++)
                //{
                    for (int j = 0; j < matrix_dimension; j++)
                    {
                        int i = desired_root;
                        if (Matrix[i, j] > 0 && (j > i))
                        {
                           origin_agenda.Add(j);

                            for (int n = 0; n < matrix_dimension; n++)
                            {
                                if (city_name_node[n].number_of_city == j)
                                {
                                    origin_name_of_node = city_name_node[n].nameof_city;
                                    origin_path = string.Join(",", origin_name_of_node, origin_path);
                                    break;
                                }
                            }
                        }
                    }
                    for (int j = 0; j < matrix_dimension; j++)
                    {
                        int i = desired_destiny;
                        if (Matrix[i, j] > 0 && (i != j))
                        {
                            destiny_agenda.Add(j);

                            for (int n = 0; n < matrix_dimension; n++)
                            {
                                if (city_name_node[n].number_of_city == j)
                                {
                                    destiny_name_of_node = city_name_node[n].nameof_city;
                                    destiny_path = string.Join(",", destiny_name_of_node, destiny_path);
                                    break;
                                }
                            }
                        }
                    }
                string[] helper = origin_path.Split(',');
                foreach(string city in helper)
                {
                    if(destiny_path.Contains(city.ToString()) && String.Compare(city, "") != 0)
                    {
                        intersected_cities = string.Join(",", intersected_cities, city);
                    }
                }
                
                if (string.Compare(intersected_cities, "") != 0)
                {
                    bi_path = string.Join(",", "Starting from the root: " + origin_path, " \n\t Starting from the goal: " + destiny_path);
                    bi_path = string.Join(",", bi_path, " \n \t The paths intersect with the following city (s): " + intersected_cities);
                    complete_path = true;
                    break;
                }


                desired_root = origin_agenda[0];
                desired_destiny = destiny_agenda[0];
                origin_agenda.RemoveAt(0);
                destiny_agenda.RemoveAt(0);

            }

            if(!complete_path)
            {
                bi_path = "There is no possible intersection between this cities";
            }
           

            return bi_path;

        }

        public static string Return_Limited_Depth(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node, int max_depth)
        {
            string depth_path = "";
            Stack Agenda = new Stack();
            int node_number = 0;
            string name_of_node = "";
            int max_depth_counter = 0;
            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {

                    if (Matrix[i, j] > 0 && (j > i))
                    {
                        Agenda.Push(j);
                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                if (!depth_path.Contains(name_of_node))
                                {
                                    depth_path = string.Join(",", name_of_node, depth_path);
                                    break;
                                }
                            }
                        }

                    }
                }
                max_depth_counter++;

                if (Agenda.Count == 0)
                {
                    depth_path = string.Join(",", "This path is incomplete: ", depth_path);
                    break;
                }

                node_number = int.Parse(Agenda.Pop().ToString());

                if (node_number == desired_destiny)
                {
                    break;
                }

                if (max_depth_counter == max_depth)
                {
                    depth_path = string.Join(",", "This path did not reach completition with the given max depth: (" + max_depth + ") ", depth_path);
                    break;
                }


                i = node_number - 1;
            }

            return depth_path;

        }

        public static string Return_Iterative_Depth(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node, int max_depth)
        {
            string depth_path = "";
            Stack Agenda = new Stack();
            int node_number = 0;
            string name_of_node = "";
            int max_depth_counter = 0;
            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {

                    if (Matrix[i, j] > 0 && (j > i))
                    {
                        Agenda.Push(j);
                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                if (!depth_path.Contains(name_of_node))
                                {
                                    depth_path = string.Join(",", name_of_node, depth_path);
                                    break;
                                }
                            }
                        }

                    }

                }
                max_depth_counter++;

                if (Agenda.Count == 0)
                {
                    depth_path = string.Join(",", "This path is incomplete: ", depth_path);
                    break;
                }

                node_number = int.Parse(Agenda.Pop().ToString());

                if (node_number == desired_destiny)
                {
                    if(max_depth_counter > max_depth)
                    {
                        depth_path = string.Join(",", "The max depth had to be raised to: (" + max_depth_counter + ") in order to reach a solution: ", depth_path);
                    }
                    break;
                }

                i = node_number - 1;
            }

            return depth_path;

        }

       public static string Return_Hill_Climbing(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string hillclimbing_path = "";
            List<int> hillclimbing_agenda = new List<int>();
            List<int> hill_costs = new List<int>();
            List<string> hill_help = new List<string>();
            int node_number = 0;
            string name_of_node = "";
            int hillclimbing_cost = 0;
            int city_number = 0;

            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {
                    if (Matrix[i, j] > 0 && (j > i)) 
                    {
                        hillclimbing_agenda.Add(j);
                        hill_costs.Add(Matrix[i, j]);
                        

                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                city_number = city_name_node[n].number_of_city;
                             
                                if (!hillclimbing_path.Contains(name_of_node))
                                {
                                    hillclimbing_path = string.Join(",", name_of_node, hillclimbing_path);
                                    hill_help.Add(Matrix[i, j].ToString() + "," + j.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                if (hillclimbing_agenda.Count > 0)
                {
                    hill_costs.Sort();
                    foreach(string node in hill_help)
                    {
                        if(int.Parse(node.Split(',')[0]) == hill_costs[0])
                        {
                            int best_node = int.Parse(node.Split(',')[1]);
                            hillclimbing_agenda.Clear();
                            hillclimbing_agenda.Add(best_node);
                        }
                    }
                    hillclimbing_cost += hill_costs[0];
                    node_number = hillclimbing_agenda[0];
                    hill_costs.Clear();
                    hill_help.Clear();
                    hillclimbing_agenda.RemoveAt(0);
                }
                else
                {
                    hillclimbing_path = string.Join(",", "This path is incomplete: ", hillclimbing_path +" \n \tThe minimum achievable cost was: (" + hillclimbing_cost.ToString() + ")");
                    break;
                }

                if (node_number == desired_destiny)
                {
                    hillclimbing_path = string.Join(",", "The minimum cost for this path is (: " + hillclimbing_cost.ToString() + ") :", hillclimbing_path);
                    break;
                }

                i = node_number - 1;
            }

            return hillclimbing_path;

        }

        public static string Return_Best_First(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string bestfirst_path = "";
            List<int> bestfirst_agenda = new List<int>();
            List<int> bestfirst_costs = new List<int>();
            List<string> bestfirst_help = new List<string>();
            int node_number = 0;
            string name_of_node = "";
            int bestfirst_cost = 0;
            int city_number = 0;

            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {
                    if (Matrix[i, j] > 0 && (j != i))
                    {
                        bestfirst_agenda.Add(j);
                        bestfirst_costs.Add(Matrix[i, j]);


                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                city_number = city_name_node[n].number_of_city;
                                if (!bestfirst_path.Contains(name_of_node))
                                {
                                    bestfirst_path = string.Join(",", name_of_node, bestfirst_path);
                                    bestfirst_help.Add(Matrix[i, j].ToString() + "," + j.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                if (bestfirst_agenda.Count > 0 && bestfirst_costs.Count > 0 && bestfirst_help.Count > 0)
                {
                    bestfirst_costs.Sort();
                    bestfirst_agenda.Clear();
                    foreach (int cost in bestfirst_costs)
                    {
                        foreach(string node in bestfirst_help)
                        {
                            if(int.Parse(node.Split(',')[0]) == cost)
                            {
                                int next_node = int.Parse(node.Split(',')[1]);
                                bestfirst_agenda.Add(next_node);
                            }
                        }
                    }

                    bestfirst_cost += bestfirst_costs[0];
                    node_number = bestfirst_agenda[0];
                    bestfirst_costs.Clear();
                    bestfirst_help.Clear();
                    bestfirst_agenda.RemoveAt(0);
                }
                else
                {
                    bestfirst_path = string.Join(",", "This path is incomplete: ", bestfirst_path + " \n \tThe minimum achievable cost was: (" + bestfirst_cost.ToString() + ")");
                    break;
                }

                if (node_number == desired_destiny)
                {
                    bestfirst_path = string.Join(",", "The minimum cost for this path is (: " + bestfirst_cost.ToString() + ") :", bestfirst_path);
                    break;
                }

                i = node_number - 1;
            }

            return bestfirst_path;

        }

        public static string Return_Beam_Search(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node, int max_kbest)
        {
            string beam_path = "";
            List<int> beamfirst_agenda = new List<int>();
            List<int> beamfirst_costs = new List<int>();
            List<string> beamfirst_help = new List<string>();
            int node_number = 0;
            string name_of_node = "";
            int beamfirst_cost = 0;
            int city_number = 0;
            int max_kbest_counter = 0;
            int agenda_counter = 0;

            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {
                    if (Matrix[i, j] > 0 && (j > i))
                    {
                        beamfirst_agenda.Add(j);
                        beamfirst_costs.Add(Matrix[i, j]);


                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                city_number = city_name_node[n].number_of_city;

                                if (!beam_path.Contains(name_of_node))
                                {
                                    beam_path = string.Join(",", name_of_node, beam_path);
                                    beamfirst_help.Add(Matrix[i, j].ToString() + "," + j.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                if (beamfirst_agenda.Count > 0 && beamfirst_costs.Count > 0 && beamfirst_help.Count > 0)
                {
                    beamfirst_costs.Sort();
                    beamfirst_agenda.Clear();
                    foreach (int cost in beamfirst_costs)
                    {
                        /* if (int.Parse(node.Split(',')[0]) == bestfirst_costs[0])
                         {
                             int best_node = int.Parse(node.Split(',')[1]);
                             bestfirst_agenda.Insert(0, best_node);
                         }*/

                        foreach (string node in beamfirst_help)
                        {
                            if (int.Parse(node.Split(',')[0]) == cost)
                            {
                                int next_node = int.Parse(node.Split(',')[1]);
                                beamfirst_agenda.Add(next_node);
                            }
                        }
                    }
                    beamfirst_agenda.Reverse();
                    agenda_counter = beamfirst_agenda.Count;
                    for (int k = 0; k <= agenda_counter; k++)
                    {
                        if (max_kbest_counter > max_kbest)
                        {
                            beamfirst_agenda.RemoveAt(0);
                        }
                        max_kbest_counter++;
                    }
                    beamfirst_agenda.Reverse();
                    beamfirst_cost += beamfirst_costs[0];
                    node_number = beamfirst_agenda[0];
                    beamfirst_costs.Clear();
                    beamfirst_help.Clear();
                    beamfirst_agenda.RemoveAt(0);
                    max_kbest_counter = 0;
                }
                else
                {
                    beam_path = string.Join(",", "This path is incomplete: ", beam_path + " \n \tThe minimum achievable cost was: (" + beamfirst_cost.ToString() + ")");
                    break;
                }

                if (node_number == desired_destiny)
                {
                    beam_path = string.Join(",", "The minimum cost for this path is (: " + beamfirst_cost.ToString() + ") :", beam_path);
                    break;
                }

                i = node_number - 1;
            }

            return beam_path;

        }

        public static string Return_BandB(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string bandb_path = "";
            string band_bestsolution = "";
            List<int> bandb_agenda = new List<int>();
            List<int> bandb_costs = new List<int>();
            List<string> bandb_help = new List<string>();
            List<string> bandb_solutions = new List<string>();
            int node_number = 0;
            string name_of_node = "";
            int bandb_cost = 0;
            int city_number = 0;
            int min_cost = 0;
            int counter = 0;

            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {
                    if (Matrix[i, j] > 0 && (j > i)) 
                    {
                        bandb_agenda.Add(j);
                        bandb_costs.Add(Matrix[i, j]);


                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                city_number = city_name_node[n].number_of_city;
                                if (!bandb_path.Contains(name_of_node))
                                {
                                    bandb_path = string.Join(",", name_of_node, bandb_path);
                                    bandb_help.Add(Matrix[i, j].ToString() + "," + j.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                if (bandb_agenda.Count > 0 && bandb_costs.Count > 0 && bandb_help.Count > 0)
                {
                    bandb_costs.Sort();
                    bandb_cost += bandb_costs[0];
                    node_number = bandb_agenda[0];
                    bandb_agenda.RemoveAt(0);
                }

               if (node_number == desired_destiny || bandb_agenda.Count == 0)
                {
                    bandb_solutions.Add(bandb_path + "+" + bandb_cost.ToString());
                    
                    foreach (string solution in bandb_solutions)
                    {
                        string mincost_helper = solution.Split('+')[1];
                        int cost = int.Parse(mincost_helper);
                        if(cost < min_cost && counter > 0)
                        {
                            min_cost = cost;
                            band_bestsolution = bandb_path;
                            band_bestsolution = string.Join(",", "The minimum cost for this path is (: " + min_cost.ToString() + ") :", band_bestsolution);
                        }
                        else if(counter == 0)
                        {
                            min_cost = cost;
                            band_bestsolution = bandb_path;
                            band_bestsolution = string.Join(",", "The minimum cost for this path is (: " + min_cost.ToString() + ") :", band_bestsolution);
                        }
                        counter++;
                    }
                    
                    bandb_path = "";
                    if(bandb_agenda.Count > 0)
                    {
                        bandb_agenda.RemoveAt(0);
                        node_number = bandb_agenda[0];
                    }
                    
                }
                
                if (bandb_agenda.Count == 0)
                    break;
                i = node_number - 1;
            }
            foreach(string paths in bandb_solutions)
            {
                bandb_path = string.Join("\n\t", paths, bandb_path);
            }
            band_bestsolution = "This was the best path: \n\t" + band_bestsolution + "\n\t This were all the possible solutions \n\t" + bandb_path;
            return band_bestsolution;

        }

        public static string Return_AStar(int[,] Matrix, int matrix_dimension, int desired_root, int desired_destiny, Node[] city_name_node)
        {
            string astar_path = "";
            string astar_bestsolution = "";
            List<int> astar_agenda = new List<int>();
            List<int> astar_costs = new List<int>();
            List<string> astar_help = new List<string>();
            List<string> astar_solutions = new List<string>();
            int node_number = 0;
            string name_of_node = "";
            int bandb_cost = 0;
            int city_number = 0;
            int min_cost = 0;
            int counter = 0;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double euclidean_distance = 0;
            double current_cost;
            int current_cost_int;

            for (int i = desired_root; i < matrix_dimension; i++)
            {
                for (int j = 0; j < matrix_dimension; j++)
                {
                    if (Matrix[i, j] > 0 && (j > i))
                    {
                        astar_agenda.Add(j);
                        current_cost = Matrix[i, j];


                        for (int n = 0; n < matrix_dimension; n++)
                        {
                            if (city_name_node[n].number_of_city == j)
                            {
                                name_of_node = city_name_node[n].nameof_city;
                                city_number = city_name_node[n].number_of_city;
                                x1 = city_name_node[i].x_coordenates;
                                y1 = city_name_node[i].y_coordenates;
                                x2 = city_name_node[n].x_coordenates;
                                y2 = city_name_node[n].y_coordenates;
                                euclidean_distance = Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2);
                                current_cost += euclidean_distance;
                                current_cost_int = int.Parse(current_cost.ToString());
                                astar_costs.Add(current_cost_int);
                                if (!astar_path.Contains(name_of_node))
                                {
                                    astar_path = string.Join(",", name_of_node, astar_path);
                                    astar_help.Add(current_cost_int.ToString() + "," + j.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
                if (astar_agenda.Count > 0 && astar_costs.Count > 0 && astar_help.Count > 0)
                {
                    astar_costs.Sort();
                    bandb_cost += astar_costs[0];
                    node_number = astar_agenda[0];
                    astar_agenda.RemoveAt(0);
                }

                if (node_number == desired_destiny || astar_agenda.Count == 0)
                {
                    astar_solutions.Add(astar_path + "+" + bandb_cost.ToString());

                    foreach (string solution in astar_solutions)
                    {
                        string mincost_helper = solution.Split('+')[1];
                        int cost = int.Parse(mincost_helper);
                        if (cost < min_cost && counter > 0)
                        {
                            min_cost = cost;
                            astar_bestsolution = astar_path;
                            astar_bestsolution = string.Join(",", "The minimum cost for this path is (: " + min_cost.ToString() + ") :", astar_bestsolution);
                        }
                        else if (counter == 0)
                        {
                            min_cost = cost;
                            astar_bestsolution = astar_path;
                            astar_bestsolution = string.Join(",", "The minimum cost for this path is (: " + min_cost.ToString() + ") :", astar_bestsolution);
                        }
                        counter++;

                    }

                    astar_path = "";
                    if (astar_agenda.Count > 0)
                    {
                        astar_agenda.RemoveAt(0);
                        node_number = astar_agenda[0];
                    }

                }

                if (astar_agenda.Count == 0)
                    break;
                i = node_number - 1;
            }
            foreach (string paths in astar_solutions)
            {
                astar_path = string.Join("\n\t", paths, astar_path);
            }
            astar_bestsolution = "This was the best path: \n\t" + astar_bestsolution + "\n\t This were all the possible solutions \n\t" + astar_path;
            return astar_bestsolution;

        }
        public struct Node
        {
            public string nameof_city;
            public int number_of_city;
            public int x_coordenates;
            public int y_coordenates;
        }



    }

    
   
   
}
