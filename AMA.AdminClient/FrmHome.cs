using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmHome : Form
    {
        private ApiService _questionsService = new ApiService("questions");
        private ApiService _answersService = new ApiService("answers");
        private ApiService _categoriesService = new ApiService("categories");

        public FrmHome()
        {
            InitializeComponent();
        }

        private async void FrmHome_Load(object sender, EventArgs e)
        {
            var questions = (await _questionsService.Get<List<QuestionResponse>>(null, "find")).ToList();
            LoadQuestions(questions);

            var categories = (await _categoriesService.Get<List<CategoryResponse>>(null, "all"));
            categories.Insert(0, new CategoryResponse { Id = 0, Name = "Select category" });
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";

        }

        private void LoadQuestions(List<QuestionResponse> questions)
        {
            PanelQuestions.Controls.Clear();
            PanelQuestions.AutoScroll = true;

            int dTM = 30;

            int pTM = 0;

            foreach (var question in questions)
            {
                int rM = 0;
                int tM = 0;

                var panelBorder = new Panel();
                panelBorder.Width = PanelQuestions.Width;
                panelBorder.BorderStyle = BorderStyle.FixedSingle;
                panelBorder.Location = new Point(rM, pTM);
                
                var questionTitle = new LinkLabel();
                questionTitle.Text = question.Title;
                questionTitle.Width = PanelQuestions.Width;
                questionTitle.Location = new Point(rM, tM);
                questionTitle.LinkBehavior = LinkBehavior.NeverUnderline;
                questionTitle.Font = new Font("Arial", 14, FontStyle.Bold);
                questionTitle.LinkClicked += (sender, EventArgs) => { questionClicked(sender, EventArgs, question); };

                tM += dTM;

                var questionAuthor = new Label();
                questionAuthor.Text = $"Created by {question.User.UserName} on {question.CreatedTime.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss")}";
                questionAuthor.Location = new Point(rM, tM);
                questionAuthor.AutoSize = true;
                questionAuthor.Font = new Font("Arial", 8, FontStyle.Bold);
                
                tM += dTM/2;

                var removeQuestion = new Button();
                removeQuestion.Text = "Remove";
                removeQuestion.Click += (sender, EventArgs) => { questionDeleted(sender, EventArgs, question); };
                removeQuestion.Location = new Point(rM, tM);
                removeQuestion.Font = new Font("Arial", 8, FontStyle.Bold);

                var editQuestion = new Button();
                editQuestion.Text = "Edit";
                editQuestion.Click += (sender, EventArgs) => { questionEdit(sender, EventArgs, question); };
                editQuestion.Location = new Point(removeQuestion.Width, tM);
                editQuestion.Font = new Font("Arial", 8, FontStyle.Bold);

                panelBorder.Height = tM + editQuestion.Height + 1; 

                panelBorder.Controls.Add(questionTitle);
                panelBorder.Controls.Add(questionAuthor);
                panelBorder.Controls.Add(removeQuestion);
                panelBorder.Controls.Add(editQuestion);

                PanelQuestions.Controls.Add(panelBorder);

                pTM += panelBorder.Height + 1;
            }
        }

        private void questionEdit(object sender, EventArgs eventArgs, QuestionResponse question)
        {
            FrmQuestion frmQuestion = new FrmQuestion(question);
            frmQuestion.Show();
            frmQuestion.FormClosing += FrmQuestion_Closing;
        }

        private async void FrmQuestion_Closing(object sender, FormClosingEventArgs e)
        {
            var questions = (await _questionsService.Get<List<QuestionResponse>>(null, "find")).ToList();
            LoadQuestions(questions);
        }

        private async void questionDeleted(object sender, EventArgs eventArgs, QuestionResponse question)
        {
            _ = await _questionsService.Delete<object>(null, $"delete/{question.ID}");

            var questions = await _questionsService.Get<List<QuestionResponse>>(null, "find");

            LoadQuestions(questions);
        }

        private async void questionClicked(object sender, LinkLabelLinkClickedEventArgs eventArgs, QuestionResponse question)
        {
            PanelAnswers.Controls.Clear();
            PanelAnswers.AutoScroll = true;

            var answers = (await _answersService.Get<List<AnswerResponse>>(null, $"all/{question.ID}")).ToList().OrderByDescending(x => x.Accepted); // order by accepted answers first;

            int rM = 0;
            int tM = 0;
            int dTM = 30;

            int pTM = 0;

            var questionLabelTitle = new RichTextBox();
            questionLabelTitle.Text = $"Title: {question.Title}";
            questionLabelTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            questionLabelTitle.BorderStyle = BorderStyle.FixedSingle;
            questionLabelTitle.Location = new Point(rM, tM);
            questionLabelTitle.Width = PanelAnswers.Width - 5;
            questionLabelTitle.BackColor = PanelAnswers.BackColor;
            questionLabelTitle.ReadOnly = true;
            using (Graphics g = CreateGraphics())
            {
                questionLabelTitle.Height = ((int)g.MeasureString(questionLabelTitle.Text,
                    questionLabelTitle.Font, questionLabelTitle.Width).Height) + 5;
            }
            PanelAnswers.Controls.Add(questionLabelTitle);

            tM += dTM;

            tM += dTM/2;

            var questionLabelBody = new RichTextBox();
            questionLabelBody.Text = $"Question: {question.Body}";
            questionLabelBody.Font = new Font("Arial", 12, FontStyle.Italic);
            questionLabelBody.Location = new Point(rM, tM);
            questionLabelBody.Width = PanelAnswers.Width - 5;
            questionLabelBody.BackColor = PanelAnswers.BackColor;
            questionLabelBody.ReadOnly = true;
            using (Graphics g = CreateGraphics())
            {
                questionLabelBody.Height = ((int)g.MeasureString(questionLabelBody.Text,
                    questionLabelBody.Font, questionLabelBody.Width).Height) + 5;
            }

            PanelAnswers.Controls.Add(questionLabelBody);

            tM += dTM;
            tM += dTM/2;

            var answersLabel = new Label();
            answersLabel.Text = "Answers";
            answersLabel.Location = new Point(rM, tM);
            answersLabel.Font = new Font("Arial", 10, FontStyle.Underline);
            PanelAnswers.Controls.Add(answersLabel);

            var addAnswer = new Button();
            addAnswer.Text = "Give answer";
            addAnswer.Click += (sender2, EventArgs) => { addAnswerClick(question); };
            addAnswer.Location = new Point(rM + answersLabel.Width + 1, tM);
            addAnswer.Font = new Font("Arial", 10, FontStyle.Bold);
            addAnswer.Width = 281;
            PanelAnswers.Controls.Add(addAnswer);

            pTM = answersLabel.Location.Y + answersLabel.Height + 5;

            foreach (var answer in answers)
            {
                var answerVotings = await _answersService.Get<List<AnswerVotingResponse>>(null, $"votings/{answer.ID}");

                rM = 0;
                tM = 0;

                var panelBorder = new Panel();
                panelBorder.Width = PanelAnswers.Width - 5;
                panelBorder.BorderStyle = BorderStyle.FixedSingle;
                panelBorder.Location = new Point(rM, pTM);

                var answerRichText = new RichTextBox();
                answerRichText.Text = $"Answer: {answer.Text}";
                answerRichText.Location = new Point(rM, tM);
                answerRichText.Width = panelBorder.Width;
                answerRichText.BackColor = PanelAnswers.BackColor;
                answerRichText.Font = new Font("Arial", 10, FontStyle.Bold);
                answerRichText.ReadOnly = true;
                if (answer.Accepted)
                    answerRichText.ForeColor = Color.Green;

                using (Graphics g = CreateGraphics())
                {
                    answerRichText.Height = ((int)g.MeasureString(answerRichText.Text,
                        answerRichText.Font, answerRichText.Width).Height) + 5;
                }

                tM += dTM;

                var answeredBy = new Label(); 
                answeredBy.Text = $"Answered by {answer.User.UserName} on {answer.TimeAnswered.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss")}";
                answeredBy.Location = new Point(rM, tM);
                answeredBy.AutoSize = true;
                answeredBy.Font = new Font("Arial", 8, FontStyle.Regular);

                tM += dTM / 2;

                var removeAnswer = new Button();
                removeAnswer.Text = "Remove";
                removeAnswer.Click += (sender2, EventArgs) => { answerDeleted(sender2, panelBorder, answer, question); };
                removeAnswer.Location = new Point(rM, tM);
                removeAnswer.Font = new Font("Arial", 10, FontStyle.Bold);


                var upAnswer = new LinkLabel();
                var downAnswer = new LinkLabel();

                upAnswer.Text = $"+ {answerVotings.Count(x => x.Rating == true)}";
                upAnswer.Click += (sender2, EventArgs) => { 
                    upAnswerEvent(sender2, downAnswer, answer);
                };
                upAnswer.Location = new Point(panelBorder.Location.X + panelBorder.Width - 100, tM);
                upAnswer.Font = new Font("Arial", 12, FontStyle.Bold);
                upAnswer.LinkBehavior = LinkBehavior.NeverUnderline;
                upAnswer.TextAlign = ContentAlignment.MiddleCenter;
                upAnswer.BorderStyle = BorderStyle.FixedSingle;
                var textMesure = TextRenderer.MeasureText(upAnswer.Text, upAnswer.Font);
                upAnswer.Width = textMesure.Width + 2;
                

                downAnswer.Text = $"- {answerVotings.Count(x => x.Rating == false)}";
                downAnswer.Click += (sender2, EventArgs) => { 
                    downAnswerEvent(sender2, upAnswer, answer); 
                };
                downAnswer.Location = new Point(upAnswer.Location.X + upAnswer.Width + 1, tM);
                downAnswer.LinkBehavior = LinkBehavior.NeverUnderline;
                downAnswer.Font = new Font("Arial", 12, FontStyle.Bold);
                downAnswer.TextAlign = ContentAlignment.MiddleCenter;
                downAnswer.BorderStyle = BorderStyle.FixedSingle;
                var textMesure2 = TextRenderer.MeasureText(downAnswer.Text, downAnswer.Font);
                downAnswer.Width = textMesure2.Width + 2;

                panelBorder.Controls.Add(upAnswer);
                panelBorder.Controls.Add(downAnswer);
                panelBorder.Controls.Add(removeAnswer);
                panelBorder.Controls.Add(answeredBy);
                panelBorder.Controls.Add(answerRichText);
                PanelAnswers.Controls.Add(panelBorder);

                panelBorder.Height = tM + answerRichText.Height + 1;

                pTM += panelBorder.Height + 1;
            }
        }

        private void addAnswerClick(QuestionResponse question)
        {
            FrmAnswer frm = new FrmAnswer(question);
            frm.Show();
            frm.FormClosing += (sender2, EventArgs) => { FrmAnswer_Closing(sender2, EventArgs, question); };

        }

        private void FrmAnswer_Closing(object sender, FormClosingEventArgs e, QuestionResponse quesiton)
        {
            questionClicked(this, new LinkLabelLinkClickedEventArgs(new LinkLabel.Link()), quesiton);
        }

        private async void upAnswerEvent(object sender2, object opositeButton, AnswerResponse answer)
        {
            var request = new
            {
                UserId = ApiService.UserId,
                AnswerId = answer.ID,
                Rating = true
            };

            var answerVotings = await _answersService.Post<List<AnswerVotingResponse>>(request, "votings/add");

            (sender2 as LinkLabel).Text = $"+ {answerVotings.Count(x => x.Rating == true)}";
            (opositeButton as LinkLabel).Text = $"- {answerVotings.Count(x => x.Rating == false)}";

        }

        private async void downAnswerEvent(object sender2, object oppositeButton, AnswerResponse answer)
        {
            var request = new
            {
                UserId = ApiService.UserId,
                AnswerId = answer.ID,
                Rating = false
            };

            var answerVotings = await _answersService.Post<List<AnswerVotingResponse>>(request, "votings/add");

            (sender2 as LinkLabel).Text = $"- {answerVotings.Count(x => x.Rating == false)}";
            (oppositeButton as LinkLabel).Text = $"+ {answerVotings.Count(x => x.Rating == true)}";
        }

        private async void answerDeleted(object sender2, Panel panel, AnswerResponse answer, QuestionResponse quesiton)
        {
            _ = await _answersService.Delete<object>(null, $"delete/{answer.ID}");

            questionClicked(this, new LinkLabelLinkClickedEventArgs(new LinkLabel.Link()), quesiton);
        }

        private void FrmUsers_Closing(object sender, FormClosingEventArgs e)
        {
            Show();
        }

        private void linkMngUsers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUsers users = new FrmUsers();
            users.Show();
            Hide();
            users.FormClosing += FrmUsers_Closing;
        }

        private void linkMngCategories_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmCategories categories = new FrmCategories();
            categories.Show();
            Hide();
            categories.FormClosing += FrmCategories_Closing;
        }
        private void FrmCategories_Closing(object sender, FormClosingEventArgs e)
        {
            Show();
        }

        private async void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int categoryId;
                bool result = int.TryParse(cmbCategory.SelectedValue.ToString(), out categoryId);
                if (result && categoryId > 0)
                {
                    var subCategories = (await _categoriesService.Get<List<SubCategoryResponse>>(null, $"sub-categories/{categoryId}"));
                    subCategories.Insert(0, new SubCategoryResponse { Id = 0, Name = "Select sub-category" });
                    cmbSubCategory.DataSource = subCategories;
                    cmbSubCategory.DisplayMember = "Name";
                    cmbSubCategory.ValueMember = "Id";
                }
                else
                    cmbSubCategory.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int categoryId;
                _ = int.TryParse(cmbCategory.SelectedValue.ToString(), out categoryId);

                int subcategoryId;
                _ = int.TryParse(cmbSubCategory.SelectedValue?.ToString(), out subcategoryId);

                var request = new 
                {
                    Title = txtSearch.Text,
                    CategoryId = categoryId,
                    SubCategoryId = subcategoryId
                };

                var questions = await _questionsService.Get<List<QuestionResponse>>(request, "find");

                LoadQuestions(questions);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkQuestions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHome home = new FrmHome();
            home.Show();
            Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void linkReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmReports frmReports = new FrmReports();
            frmReports.Show();
            Hide();
            frmReports.FormClosing += FrmReports_Closing;
        }

        private void FrmReports_Closing(object sender, FormClosingEventArgs e)
        {
            Show();
        }
    }
}
