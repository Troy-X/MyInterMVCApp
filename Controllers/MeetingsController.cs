using Microsoft.AspNetCore.Mvc;
using MyInterMVCApp.Models;
using MyInterMVCApp.Data;
using MyInterMVCApp.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyInterMVCApp.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly AppDbContext _context;

        public MeetingsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string userTimeZone = "Africa/Lagos"; // Later this comes from user profile

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(userTimeZone);

            var meetings = _context.Meetings
                .Select(m => new MeetingDisplayViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    LocalStartTime = TimeZoneInfo.ConvertTimeFromUtc(
                        m.StartTimeUtc,
                        tz
                    ),
                    TimeZoneId = userTimeZone
                })
                .ToList();

            return View(meetings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMeetingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Convert local time to UTC
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(model.TimeZoneId);

            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(
                model.LocalStartTime,
                tz
            );

            var meeting = new Meeting
            {
                Title = model.Title,
                StartTimeUtc = utcTime,
                CreatedByTimeZone = model.TimeZoneId
            };

            _context.Meetings.Add(meeting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var meeting = _context.Meetings.Find(id);
            if (meeting == null) return NotFound();

            var tz = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            // later this can be user tz

            var model = new MeetingViewModel
            {
                Id = meeting.Id,
                Title = meeting.Title,
                StartTimeLocal = TimeZoneInfo.ConvertTimeFromUtc(
                    meeting.StartTimeUtc, tz),
                TimeZoneId = tz.Id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MeetingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var meeting = _context.Meetings.Find(model.Id);
            if (meeting == null) return NotFound();

            var tz = TimeZoneInfo.FindSystemTimeZoneById(model.TimeZoneId);

            meeting.Title = model.Title;
            meeting.StartTimeUtc = TimeZoneInfo.ConvertTimeToUtc(
                model.StartTimeLocal, tz);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var meeting = _context.Meetings.Find(id);
            if (meeting == null) return NotFound();

            var model = new MeetingViewModel
            {
                Id = meeting.Id,
                Title = meeting.Title
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(MeetingViewModel model)
        {
            var meeting = _context.Meetings.Find(model.Id);
            if (meeting == null) return NotFound();

            _context.Meetings.Remove(meeting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }






    }
}
