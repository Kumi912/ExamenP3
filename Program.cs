using System;
using System.IO;
using System.Text.Json;
class Venta
{
    public string codigo { get; set; }
    public string nombre { get; set; }
    public int stock { get; set; }
    public double precioUnitario { get; set; }

    public Venta(string Nombre, string Codigo, int Stock, double PrecioUnitario) //Constructor
    {
        nombre = Nombre;
        codigo = Codigo;
        stock = Stock;
        precioUnitario = PrecioUnitario;
    }

    public static Venta Productos() //Metodo para ingresar los productos
    {
        Console.WriteLine("Ingrese el nombre del producto"); //Pedimos al usuario el nombre del producto
        string nombre = Console.ReadLine(); //Guardamos el nombre del producto en la variable nombre

        Console.WriteLine("Ingrese el codigo del producto"); //Pedimos al usuario el codigo del producto
        string codigo = Console.ReadLine(); //Guardamos el codigo del producto en la variable codigo
    
        Console.WriteLine("Ingrese la cantidad que tiene en stock"); //Pedimos al usuario la cantidad que tiene en stock
        int stock = int.Parse(Console.ReadLine()); //Guardamos la cantidad de stock en la variable stock

        Console.WriteLine("Ingrese el precio unitario"); //Pedimos al usuario el precio unitario
        double precioUnitario = Convert.ToDouble(Console.ReadLine()); //Guardamos el precio unitario en la variable precioUnitario
        
        return new Venta(nombre, codigo, stock, precioUnitario); //Retornamos el objeto Venta  
    }

    public static void MostrarInformacion(string Ventas) //Metodo para calcular el total de ventas
    {
        if (File.Exists(Ventas)) //Si el archivo existe
        {
            string contenido = File.ReadAllText(Ventas); //Guardamos el contenido del archivo en una variable
            Venta [] ventas = JsonSerializer.Deserialize<Venta []>(contenido); //Deserializamos el contenido del archivo en una lista de objetos Venta

            Console.WriteLine("Los productos en la lista son \nCodigo Nombre Stock Precio unitario"); //Mostramos los encabezados de la tabla
            foreach (Venta venta in ventas) //Recorremos la lista de ventas
            {
                Console.WriteLine(venta.codigo + "," + venta.nombre + "," + venta.stock + "," + venta.precioUnitario); //Mostramos los datos de la venta
            }
        }
    }

    public void BuscarProducto(string Venta) //Metodo para buscar un producto
    {
        if (File.Exists(Venta)) //Si el archivo existe
        {
            string contenido = File.ReadAllText(Venta); //Guardamos el contenido del archivo en una variable
            Venta [] ventas = JsonSerializer.Deserialize<Venta []>(contenido); //Deserializamos el contenido del archivo en una lista de objetos Venta

            Console.WriteLine("Ingrese el codigo del producto que quiere busca");
            string buscar = Console.ReadLine();
            
            for (int i = 0; i < ventas.Length; i++) //Bucle que recorre el arreglo de codigo
            {
                if (ventas[i].codigo == buscar) //Condición para saber si el codigo se encuentra en el arreglo
                {
                    Console.WriteLine("El codigo " + buscar + " se encuentra en el stock");
                }
                else
                {
                    Console.WriteLine("El codigo " + buscar + "no se encuentra en el stock");
                }
            }
        }
    }

    public void Comprar()
    {
        if (File.Exists(Venta)) //Si el archivo existe
        {
            string contenido = File.ReadAllText(Venta); //Guardamos el contenido del archivo en una variable
            Venta [] ventas = JsonSerializer.Deserialize<Venta []>(contenido); //Deserializamos el contenido del archivo en una lista de objetos Venta
            
            Console.WriteLine("Ingrese el producto que quiere comprar");
            string producto = Console.ReadLine();
            
            Console.WriteLine("Ingrese la cantidad que quiere comprar");
            int cantidad = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < ventas.Length; i++)
            {
                if (ventas[i].nombre == producto)
                {
                    ventas[i].stock - cantidad;
                }
            }
        }
    }

    public void Pagar()
    {
        if (File.Exists(Venta)) //Si el archivo existe
        {
            string contenido = File.ReadAllText(Venta); //Guardamos el contenido del archivo en una variable
            Venta [] ventas = JsonSerializer.Deserialize<Venta []>(contenido); //Deserializamos el contenido del archivo en una lista de objetos Venta
            
            Comprar();
            
        }
    }
}

class Program
{
    static void Main()
    {
        string Ventas = "ventas.json"; //Nombre del archivo donde se guardaran los productos
        
        Console.WriteLine("¿Cuantos productos quiere ingresar?"); //Preguntamos al usuario cuantos productos quiere ingresar
        int cantidad = int.Parse(Console.ReadLine()); //Guardamos la cantidad de productos en la variable cantidad
        
        Venta [] listaVentas = new Venta[cantidad]; //Lista para guardar los productos
        
        
        for (int i = 0; i < cantidad; i++) //Ciclo para ingresar los productos
        {
            listaVentas[i] = Venta.Productos(); //Guardamos el producto en la lista de ventas
        }

        //WriteIndented = true lo ponemos para que el archivo quede con un formato legible
        JsonSerializerOptions opciones = new JsonSerializerOptions {WriteIndented = true}; //Creamos un objeto de la clase JsonSerializerOptions
        string guardar = JsonSerializer.Serialize(listaVentas, opciones); //Serializamos la lista de ventas en un string

        File.WriteAllText(Ventas, guardar); //Guardamos el string en el archivo ventas.json
        
        Venta.MostrarInformacion(Ventas); //Llamamos al metodo MostrarInformacion para mostrar los datos de los productos   
    }
}
