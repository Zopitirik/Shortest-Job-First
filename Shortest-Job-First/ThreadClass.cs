//Name : Hikmet ERGÜN
//E-mail : hikmet@hikmetergun.net

using System.Threading;

namespace ShortestJobFirst
{
    public class ThreadClass
    {
        public int ThreadNumber; //Thread Number
        public Thread _Thread; //Initializes a new instance of the Thread class.
        public ThreadStart _ThreadStart; //When a managed thread is created, the method that executes on the thread is represented by a ThreadStart delegate
        public int WorkingTime; //Thread will work until it equals to zero

        public void DoProccess()
        {
            int counter = 0;
            for (int i = 0; i < 1000000; i++)
            {
                for (int j = 0; j < 1000000; j++)
                {
                    counter++;
                }
            }
        } //Thread Process
        public ThreadClass() //ThreadClass Constructor
        {
            this._ThreadStart = new ThreadStart(DoProccess); //Create Delegate
            this._Thread = new Thread(_ThreadStart); //Create Thread
        }
    }
}