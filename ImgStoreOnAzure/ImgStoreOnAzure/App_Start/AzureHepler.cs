using System;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Web;
using Microsoft.WindowsAzure;
using System.Configuration;

public class AzureHelper : ApiController
{
    static string containerName = ConfigurationManager.AppSettings["ContainerName"];
    static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
    static CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
    static CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


    public string UploadImage(HttpPostedFileBase postedImage, int partnerId, int selectedClient)
    {
        string path = Convert.ToString(partnerId + "/" + selectedClient + "/");
        string imageUrl = "";
        string result = "";
        try
        {
            if (cloudBlobContainer.CreateIfNotExists())
            {
                cloudBlobContainer.SetPermissions(
                   new BlobContainerPermissions
                   {
                       PublicAccess = BlobContainerPublicAccessType.Blob
                   });
            }
            string imageName = Guid.NewGuid().ToString() + "-" + Path.GetExtension(postedImage.FileName);
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(path + imageName);
            cloudBlockBlob.Properties.ContentType = postedImage.ContentType;
            cloudBlockBlob.UploadFromStream(postedImage.InputStream);
            imageUrl = cloudBlockBlob.Uri.ToString();
            if (imageUrl != "")
            {
                result = imageUrl;
            }

        }
        catch (Exception ex)
        {

            return result = imageUrl;
        }
        return imageUrl;
    }
}
