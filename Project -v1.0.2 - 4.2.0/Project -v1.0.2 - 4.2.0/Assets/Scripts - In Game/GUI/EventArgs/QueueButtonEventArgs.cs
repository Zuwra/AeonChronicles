using UnityEngine;
using System.Collections;
using System;

public class QueueButtonEventArgs : EventArgs {

	public int QueueNumber;
	
	public QueueButtonEventArgs(int queueNumber)
	{
		QueueNumber = queueNumber;
	}
}
