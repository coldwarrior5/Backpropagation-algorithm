using System;

namespace Backpropagation.Structures
{
	public enum BackpropagationType
	{
		Batch,
		Online,
		MiniBatch
	}

	public static class Backpropagation
	{
		public static string ToString(BackpropagationType type)
		{
			switch (type)
			{
				case BackpropagationType.Batch:
					return "Batch";
				case BackpropagationType.MiniBatch:
					return "Mini-Batch";
				case BackpropagationType.Online:
					return "Online";
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

		public static BackpropagationType ToEnum(String type)
		{
			if (type.Equals(BackpropagationType.Batch.ToString()))
				return BackpropagationType.Batch;
			if (type.Equals(BackpropagationType.MiniBatch.ToString()))
				return BackpropagationType.MiniBatch;
			if (type.Equals(BackpropagationType.Online.ToString()))
				return BackpropagationType.Online;
			return BackpropagationType.MiniBatch;
		}
	}
}