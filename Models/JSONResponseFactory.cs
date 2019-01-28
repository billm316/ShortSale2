using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortSale2.Models
{
    /*
     * This code simulates a click event that posts into our controller using jQuery.
     * Because the response (data) is automatically converted into an jQuery object, 
     * you can use it immediately. Notice that I am calling the property names that 
     * I assign on the factory such as “Success”, “ErrorMessage” and “Object”.
     * Inside the data.Object there will be any object that you specified on your server side 
     * code, and you can use it accordingly. In the snippet above, we are assuming the 
     * object will have a property called “Name”.
     * 
        $(function () {
           $(".test-link").click(function (e) {
             e.preventDefault;
             $.post("controller/action", function (data) {
                if (data.Success == true) {
                   alert("Success");
                   if (data.Object != null){
                      //here I could call the properties of my object, as below:
                      alert(data.Object.Name); //assuming your object has a property name
                    }
                 }
                 else {alert(data.ErrorMessage);}
               });
               return false;
            });
        });
     */
    public class JSONResponseFactory
    {
        /*
         * This function generates an object that has a Success flag, which is set to false 
         * (is an error), and an ErrorMessage. We force the error responses to have an error
         * message, although you can chose to do it differently.
         */
        public static object ErrorResponse(string error)
        {
            return new { Success = false, ErrorMessage = error };
        }

        /*
         * The function generates an object with Success equals to true and that is it. 
         * It is used a lot for functions such as “Delete”.
         */
        public static object SuccessResponse()
        {
            return new { Success = true };
        }

        /*
         * the function that generates a response with a reference object inside. 
         * This function is useful when you need to send an object (Get type of funcitons).
         */
        public static object SuccessResponse(object referenceObject)
        {
            return new { Success = true, Object = referenceObject };
        }
    }
}