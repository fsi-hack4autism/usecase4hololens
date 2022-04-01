using facialcues.Entity;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace facialcues
{
    public class VideoManager
    {
        IFaceClient faceClient;
        private bool Authenticate()
        {
            if (null == faceClient)
                faceClient = new FaceClient(new ApiKeyServiceClientCredentials(Constants.FaceApiKey)) { Endpoint = Constants.FaceApiEndPoint };
            return null != faceClient;
        }
        public Task<List<VideoFrame2>> SplitViedoIntoFrames(string videoUrl)
        {
            if (!Authenticate()) return new List<VideoFrame2>();
            var videoFrames = new List<VideoFrame2>();
            // Create grabber. 
            FrameGrabber<DetectedFace[]> grabber = new FrameGrabber<DetectedFace[]>();
            // Set up a listener for when we acquire a new frame.
            grabber.NewFrameProvided += (s, e) =>
            {
                Console.WriteLine($"New frame acquired at {e.Frame.Metadata.Timestamp}");
            };

            // Set up Face API call.
            grabber.AnalysisFunction = async frame =>
            {
                Console.WriteLine($"Submitting frame acquired at {frame.Metadata.Timestamp}");
                // Encode image and submit to Face API. 
                return (await faceClient.Face.DetectWithStreamAsync(frame.Image.ToMemoryStream(".jpg"))).ToArray();
            };
            // Set up a listener for when we receive a new result from an API call. 
            grabber.NewResultAvailable += (s, e) =>
            {
                if (e.TimedOut)
                    Console.WriteLine("API call timed out.");
                else if (e.Exception != null)
                    Console.WriteLine($"API call threw an exception: {e.Exception}");
                else
                    Console.WriteLine($"New result received for frame acquired at {e.Frame.Metadata.Timestamp}. {e.Analysis.Length} faces detected");
            };

            // Tell grabber when to call API.
            // See also TriggerAnalysisOnPredicate
            grabber.TriggerAnalysisOnInterval(TimeSpan.FromMilliseconds(3000));

            return videoFrames;
        }

        private async Task<VideoFrame2> GrabFrame(string videoUrl)
        {

        }
    }
}
