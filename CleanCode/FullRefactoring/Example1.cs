using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace CleanCode.FullRefactoring
{
    public class PostRepository
    {
        
        private readonly PostDbContext _dbContext;


        public PostRepository()
        {
            _dbContext = new PostDbContext();
        }

        public Post GetPost(int? postId, string postTitle, string postBody)
        {
            
            return new Post()
            {
                Id = Convert.ToInt32(postId),
                Title = postTitle,
                Body = postBody
            };
        }

        public Post GetPost(int postId)
        {
            return _dbContext.Posts.SingleOrDefault(p => p.Id == postId);
        }

        public void SavePost(Post post)
        {
            this._dbContext.Posts.Add(post);
            this._dbContext.SaveChanges();
        }
    }

    public class PostControl : System.Web.UI.UserControl
    {
        private readonly PostRepository _postRepository;
        private readonly PostValidator _postValidator;

        public PostControl()
        {
            _postRepository = new PostRepository();
            _postValidator = new PostValidator();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Page.IsPostBack)
            {
                var post      = _postRepository.GetPost(PostId, postBody:PostBody.Text, postTitle:PostTitle.Text);
                var results   = _postValidator.Validate(post);

                if (results.IsValid)
                {
                    _postRepository.SavePost(post);
                }
                else
                {
                    DisplayErrors(results);
                }
            }
            else
            {
                DisplayPost();
            }
        }

        private void DisplayErrors(ValidationResult results)
        {
            var summary = (BulletedList)FindControl("ErrorSummary") == null
                ? new BulletedList()
                : (BulletedList)FindControl("ErrorSummary");




            foreach (var error in results.Errors)
            {
                DisplayError(error, summary);
            }
        }

        private void DisplayError(ValidationError error, BulletedList summary)
        {
            var errorLabel = GetErrorLabelControl(error);

            if (errorLabel == null)
            {
                summary.Items.Add(new ListItem(error.ErrorMessage));
            }
            else
            {
                errorLabel.Text = error.ErrorMessage;
            }
        }

        private Label GetErrorLabelControl(ValidationError error)
        {
            return FindControl(error.PropertyName + "Error") as Label;
        }

        private void DisplayPost()
        {
            var postId = Convert.ToInt32(Request.QueryString["id"]);
            var post = _postRepository.GetPost(postId);
            if (post != null)
            {
                if (PostBody != null)
                    PostBody.Text = post.Body;
                if (PostTitle != null)
                    PostTitle.Text = post.Title;
            }
                
        }

        public Label PostBody { get; set; }

        public Label PostTitle { get; set; }

        public int? PostId { get; set; }
    }

    #region helpers

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class PostValidator
    {
        public ValidationResult Validate(Post entity)
        {
            throw new NotImplementedException();
        }
    }

    public class DbSet<T> : IQueryable<T>
    {
        public void Add(T entity)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class PostDbContext
    {
        public DbSet<Post> Posts { get; set; }

        public void SaveChanges()
        {
        }
    }
    #endregion

}