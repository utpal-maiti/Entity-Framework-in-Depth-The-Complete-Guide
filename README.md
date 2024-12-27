# Entity Framework in Depth The Complete Guide+

### Lazy Loading in Entity Framework

**Lazy Loading** is a pattern where the related data is not loaded from the database until it is accessed. This can be useful in certain scenarios but also has its downsides.

#### Best Practices:

1. **Use when loading an object graph is costly**: Lazy loading can save resources by loading related entities only when necessary.
2. **Use in Desktop Applications**: Desktop applications often benefit from lazy loading as they handle smaller, more manageable loads and have stateful user interactions.
3. **Avoid in Web Applications**: Web applications should avoid lazy loading due to the overhead of multiple database calls which can degrade performance.

#### How to Stop Lazy Loading:

- **Don't mark the property with `virtual`**:

  ```csharp
  public class Project
  {
      public int ProjectId { get; set; }
      public string Name { get; set; }
      public ICollection<WorkItem> WorkItems { get; set; } // Without virtual
  }
  ```

- **Disable Lazy Loading from Configuration**:
  ```csharp
  public class Plutodb_Context : DbContext
  {
      public Plutodb_Context()
      {
          this.Configuration.LazyLoadingEnabled = false;
      }
  }
  ```

### Eager Loading

**Eager Loading** is a pattern where the related data is loaded from the database as part of the initial query. This typically results in a single query with `JOINs`.

#### Best Practices:

1. **Uses JOINs**: Eager loading fetches related data using SQL JOINs, which is efficient for retrieving complete object graphs.
2. **One Round-Trip**: Eager loading makes a single call to the database, reducing the number of round-trips.
3. **Use in Web Applications**: It is best suited for web applications to minimize the number of database calls.

#### Examples:

- **For Single Properties**:

  ```csharp
  var courses = context.Courses.Include(c => c.Author.Address);
  ```

- **For Collection Properties**:

  ```csharp
  var courses = context.Courses.Include(a => a.Tags);
  ```

- **Multiple Levels of Eager Loading**:
  ```csharp
  var courses = context.Courses
                      .Include(c => c.Author.Address)
                      .Include(a => a.Tags.Select(t => t.Moderator))
                      .Include(c => c.Category)
                      .Include(c => c.Cover);
  ```

### Explicit Loading

**Explicit Loading** is a pattern where related data is explicitly loaded from the database at a later time, using separate queries.

#### Best Practices:

1. **Separates Queries**: Explicit loading uses separate queries to load related entities, which can be useful when you don't need all related data upfront.
2. **Multiple Round-Trips**: This approach results in multiple calls to the database, which may be necessary in some scenarios but should be used judiciously to avoid performance issues.

#### Example:

```csharp
// Load a single course
var course = context.Courses.Single(c => c.Id == 1);

// Load related data
context.Entry(course).Collection(c => c.Tags).Load();
context.Entry(course).Reference(c => c.Author).Load();
```

### Conclusion

Understanding when and how to use lazy, eager, and explicit loading in Entity Framework can greatly impact the performance and efficiency of your application. Choosing the right loading strategy depends on the specific requirements and context of your application. Always consider the trade-offs and best practices for each approach to ensure optimal performance.
