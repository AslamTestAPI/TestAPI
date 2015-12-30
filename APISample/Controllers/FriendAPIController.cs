using APISample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APISample.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    public class FriendAPIController : ApiController
    {
        private FriendDBContext db = new FriendDBContext();

        [HttpGet]
        public IEnumerable<Friend> Get()
        {
            return db.Friends.AsEnumerable();
        }

        public Friend Get(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return friend;
        }

        public HttpResponseMessage Post(Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Friends.Add(friend); 
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, friend);
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new Friend { FriendId = friend.FriendId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        public HttpResponseMessage Put(Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //if (id != friend.FriendId)
            //{
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);
            //} .lklsdklf

            db.Entry(friend).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            Friend friend = db.Friends.Find(id);
            if (friend == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Friends.Remove(friend);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, friend);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
