<template>
  <q-page class="q-pa-md">
    <div v-if="weatherData" class="weather-container">
      <div class="weather-header">
        <h2>{{ weatherData.name }}, {{ weatherData.sys.country }}</h2>
        <p class="weather-description">{{ weatherData.weather[0].description }}</p>
      </div>

      <div class="weather-details">
        <div class="weather-temp">
          <p><strong>Temperature:</strong> {{ weatherData.main.temp.toFixed(1) }}°C</p>
          <p><strong>Feels Like:</strong> {{ weatherData.main.feels_Like.toFixed(1) }}°C</p>
        </div>

        <div class="weather-stats">
          <p><strong>Humidity:</strong> {{ weatherData.main.humidity }}%</p>
          <p><strong>Wind Speed:</strong> {{ weatherData.wind.speed }} m/s</p>
        </div>
      </div>

      <div class="weather-footer">
        <p>Current time: <span class="updated-time">{{ formattedTime }}</span></p>
      </div>
    </div>

    <div v-else>
      <q-spinner color="primary" size="50px" />
    </div>
  </q-page>
</template>

<script>
  import axios from 'axios';
  import api from '@/api';

  export default {
    name: 'HomePage',
    data() {
      return {
        weatherData: null,
        formattedTime: null,
      };
    },
    mounted() {
      this.fetchWeatherData();
      this.updateCurrentTime();
    },
    methods: {
      async fetchWeatherData() {
        try {
          // Hardcoded coordinates for Craiova
          const lat = 44.3302;
          const lon = 23.7949;

          const response = await api.get('/WeatherForecast/Current', {
            params: {
              lat,
              lon,
            },
          });

          this.weatherData = response.data;
        } catch (error) {
          console.error('Error fetching weather data:', error);
        }
      },
      updateCurrentTime() {
        const now = new Date();
        this.formattedTime = now.toLocaleString();
      },
    },
  };
</script>

<style scoped>
  .weather-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 1.5rem;
    background-color: #ffffff;
    border-radius: 12px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
    max-width: 400px;
    margin: 0 auto;
  }

  .weather-header {
    text-align: center;
    color: #333;
  }

  .weather-description {
    font-size: 1.1rem;
    font-weight: 500;
    color: #888;
  }

  .weather-details {
    margin-top: 2rem;
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .weather-temp, .weather-stats {
    width: 100%;
    text-align: left;
    margin-bottom: 1rem;
    font-size: 1rem;
  }

    .weather-temp p, .weather-stats p {
      margin: 5px 0;
      color: #444;
    }

    .weather-temp strong, .weather-stats strong {
      color: #0077cc;
    }

  .weather-footer {
    margin-top: 1rem;
    font-size: 0.9rem;
    color: #555;
    text-align: center;
  }

  .updated-time {
    font-style: italic;
    color: #999;
  }

  .q-spinner {
    display: block;
    margin: 0 auto;
  }
</style>
