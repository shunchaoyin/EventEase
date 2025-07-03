using EventEase.Models;

namespace EventEase.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventItem);
        Task<Event> UpdateEventAsync(Event eventItem);
        Task<bool> DeleteEventAsync(int id);
        Task<List<Event>> SearchEventsAsync(string searchTerm);
        Task<List<Event>> GetEventsByCategoryAsync(string category);
        Task<bool> RegisterForEventAsync(int eventId, UserRegistration registration);
    }

    public class EventService : IEventService
    {
        private static List<Event> _events = new List<Event>();
        private static List<UserRegistration> _registrations = new List<UserRegistration>();
        private static int _nextEventId = 1;
        private static int _nextRegistrationId = 1;

        public EventService()
        {
            // 初始化模拟数据
            InitializeMockData();
        }

        private void InitializeMockData()
        {
            if (_events.Count == 0)
            {
                _events.AddRange(new List<Event>
                {
                    new Event
                    {
                        Id = _nextEventId++,
                        Name = "技术创新大会",
                        Date = DateTime.Now.AddDays(30),
                        Location = "北京国际会议中心",
                        Description = "探讨最新技术趋势和创新解决方案",
                        Capacity = 200,
                        CurrentAttendees = 45,
                        Price = 299.00m,
                        Category = "技术",
                        ImageUrl = "/images/tech-conference.jpg"
                    },
                    new Event
                    {
                        Id = _nextEventId++,
                        Name = "企业年度庆典",
                        Date = DateTime.Now.AddDays(15),
                        Location = "上海大剧院",
                        Description = "庆祝公司成立十周年，员工共庆佳节",
                        Capacity = 500,
                        CurrentAttendees = 120,
                        Price = 0,
                        Category = "庆典",
                        ImageUrl = "/images/celebration.jpg"
                    },
                    new Event
                    {
                        Id = _nextEventId++,
                        Name = "数字营销峰会",
                        Date = DateTime.Now.AddDays(45),
                        Location = "深圳会展中心",
                        Description = "数字化转型时代的营销策略与实践",
                        Capacity = 300,
                        CurrentAttendees = 89,
                        Price = 599.00m,
                        Category = "营销",
                        ImageUrl = "/images/marketing-summit.jpg"
                    },
                    new Event
                    {
                        Id = _nextEventId++,
                        Name = "团队建设活动",
                        Date = DateTime.Now.AddDays(7),
                        Location = "杭州西湖景区",
                        Description = "户外拓展训练，增强团队凝聚力",
                        Capacity = 50,
                        CurrentAttendees = 35,
                        Price = 150.00m,
                        Category = "团建",
                        ImageUrl = "/images/team-building.jpg"
                    },
                    new Event
                    {
                        Id = _nextEventId++,
                        Name = "产品发布会",
                        Date = DateTime.Now.AddDays(60),
                        Location = "广州琶洲展馆",
                        Description = "全新产品线发布，引领行业新潮流",
                        Capacity = 400,
                        CurrentAttendees = 0,
                        Price = 0,
                        Category = "产品",
                        ImageUrl = "/images/product-launch.jpg"
                    }
                });
            }
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            await Task.Delay(100); // 模拟异步操作
            return _events.Where(e => e.IsActive).OrderBy(e => e.Date).ToList();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            await Task.Delay(50);
            return _events.FirstOrDefault(e => e.Id == id && e.IsActive);
        }

        public async Task<Event> CreateEventAsync(Event eventItem)
        {
            await Task.Delay(100);
            eventItem.Id = _nextEventId++;
            _events.Add(eventItem);
            return eventItem;
        }

        public async Task<Event> UpdateEventAsync(Event eventItem)
        {
            await Task.Delay(100);
            var existingEvent = _events.FirstOrDefault(e => e.Id == eventItem.Id);
            if (existingEvent != null)
            {
                var index = _events.IndexOf(existingEvent);
                _events[index] = eventItem;
            }
            return eventItem;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            await Task.Delay(100);
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null)
            {
                eventItem.IsActive = false;
                return true;
            }
            return false;
        }

        public async Task<List<Event>> SearchEventsAsync(string searchTerm)
        {
            await Task.Delay(100);
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllEventsAsync();

            return _events.Where(e => e.IsActive &&
                (e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                 e.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                 e.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(e => e.Date)
                .ToList();
        }

        public async Task<List<Event>> GetEventsByCategoryAsync(string category)
        {
            await Task.Delay(100);
            if (string.IsNullOrWhiteSpace(category))
                return await GetAllEventsAsync();

            return _events.Where(e => e.IsActive && e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.Date)
                .ToList();
        }

        public async Task<bool> RegisterForEventAsync(int eventId, UserRegistration registration)
        {
            await Task.Delay(100);
            var eventItem = _events.FirstOrDefault(e => e.Id == eventId && e.IsActive);
            if (eventItem != null && !eventItem.IsFull)
            {
                registration.Id = _nextRegistrationId++;
                registration.EventId = eventId;
                registration.Event = eventItem;
                _registrations.Add(registration);
                eventItem.CurrentAttendees++;
                return true;
            }
            return false;
        }
    }
}
