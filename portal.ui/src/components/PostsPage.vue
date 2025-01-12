<template>
  <q-page class="q-pa-md">
    <div class="posts-container">
      <h2>Posts</h2>
      <p>Here are some posts!</p>

      <!-- Create Post Section -->
      <div v-if="currentUser" class="create-post">
        <h3>Create a New Post</h3>
        <q-input v-model="newPostMessage"
                 placeholder="What's on your mind?"
                 class="q-mb-md"
                 type="textarea"
                 rows="4"
                 filled
                 dense
                 autofocus />
        <div>
          <q-btn @click="createPost" label="Create Post" color="primary" class="full-width-btn" />
        </div>
      </div>

      <!-- Display Posts -->
      <div v-for="post in posts" :key="post.postID" class="post-card">
        <div class="post-header">
          <h3>{{ post.authorName }}</h3>
          <span class="post-date">{{ formatDate(post.creationDate) }}</span>
        </div>

        <!-- Post Message Section (Editable if clicked) -->
        <div class="post-message">
          <div v-if="post.isEditing">
            <q-input v-model="post.updatedMessage"
                     placeholder="Edit your post message"
                     type="textarea"
                     rows="4"
                     filled
                     dense />
            <div class="post-actions">
              <q-btn @click="savePost(post)" label="Save" color="primary" flat />
              <q-btn @click="cancelEdit(post)" label="Cancel" color="negative" flat />
            </div>
          </div>
          <div v-else>
            <p>{{ post.message }}</p>
          </div>
        </div>

        <!-- Update and Delete Actions -->
        <div v-if="post.authorID === currentUser.id" class="post-actions">
          <q-btn @click="editPost(post)" label="Edit" color="primary" flat />
          <q-btn @click="deletePost(post.postID)" label="Delete" color="negative" flat />
        </div>
      </div>
    </div>
  </q-page>
</template>

<script>
  import api from '@/api';

  export default {
    name: 'PostsPage',
    data() {
      return {
        posts: [],
        currentUser: null,
        newPostMessage: '',
      };
    },
    mounted() {
      this.fetchPosts();
      this.fetchCurrentUser();
    },
    methods: {
      async fetchPosts() {
        try {
          const response = await api.get('Post');
          const posts = response.data;

          // Fetch author names for each post
          for (let post of posts) {
            const userResponse = await api.get(`User/${post.authorID}`);
            post.authorName = userResponse.data.username;
            post.updatedMessage = post.message;  // Save the original message for later use
            post.isEditing = false;  // Initially, posts are not in editing mode
          }

          this.posts = posts;
        } catch (error) {
          console.error('Error fetching posts:', error);
        }
      },
      async fetchCurrentUser() {
        try {
          const response = await api.get('User/me');
          this.currentUser = response.data;
        } catch (error) {
          console.error('Error fetching current user:', error);
        }
      },
      formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleString();
      },
      async createPost() {
        if (!this.newPostMessage) return;

        try {
          const response = await api.post('Post', {
            message: this.newPostMessage,
          });

          const newPost = response.data;
          const userResponse = await api.get(`User/${newPost.authorID}`);
          newPost.authorName = userResponse.data.username;
          newPost.updatedMessage = newPost.message;
          newPost.isEditing = false;

          this.posts.push(newPost);  // Add the new post to the list
          this.newPostMessage = '';  // Clear the input field
        } catch (error) {
          console.error('Error creating post:', error);
        }
      },
      editPost(post) {
        post.isEditing = true;  // Set post to editing mode
      },
      cancelEdit(post) {
        post.isEditing = false;  // Cancel editing and revert to original message
        post.updatedMessage = post.message;
      },
      async savePost(post) {
        if (!post.updatedMessage || post.updatedMessage === post.message) return; // No change or empty message

        try {
          const response = await api.put(`Post/${post.postID}`, {
            message: post.updatedMessage,
          });

          const updatedPost = response.data;
          const postIndex = this.posts.findIndex(p => p.postID === updatedPost.postID);

          // Preserve the authorName after update
          const authorResponse = await api.get(`User/${updatedPost.authorID}`);
          updatedPost.authorName = authorResponse.data.username;

          // Update the post in the list
          this.posts[postIndex] = updatedPost;

          post.isEditing = false;  // Exit editing mode
        } catch (error) {
          console.error('Error updating post:', error);
        }
      },
      async deletePost(postID) {
        try {
          await api.delete(`Post/${postID}`);
          this.posts = this.posts.filter(post => post.postID !== postID);
        } catch (error) {
          console.error('Error deleting post:', error);
        }
      },
    },
  };
</script>

<style scoped>
  .posts-container {
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
    max-width: 800px;
    margin: 0 auto;
  }

  .post-card {
    background-color: #ffffff;
    border-radius: 10px;
    padding: 1.5rem;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }

  .post-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

    .post-header h3 {
      margin: 0;
      color: #0077cc;
      font-size: 1.2rem;
    }

    .post-header .post-date {
      font-size: 0.9rem;
      color: #777;
    }

  .post-message p {
    font-size: 1rem;
    color: #333;
    margin-top: 1rem;
  }

  .post-actions {
    display: flex;
    gap: 1rem;
    margin-top: 1rem;
  }

  .create-post {
    background-color: #f9f9f9;
    padding: 1.5rem;
    border-radius: 8px;
    margin-bottom: 1.5rem;
  }

    .create-post h3 {
      margin-bottom: 1rem;
    }

  .q-spinner {
    display: block;
    margin: 0 auto;
  }

  .full-width-btn {
    width: 100%;
    margin-top: 1rem;
  }

  .q-btn--negative {
    width: auto;
  }
</style>
