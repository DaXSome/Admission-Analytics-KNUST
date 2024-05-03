<script lang="ts">
  import { onMount } from "svelte";
  import { fly, fade } from "svelte/transition";
  import ChartDataLabels from "chartjs-plugin-datalabels";
  import chartjs from "chart.js/auto";

  interface Node {
    obuasi: number;
    main: number;
  }

  export let campus: Node;

  let chartCanvas: HTMLCanvasElement;

  $: campussText = [] as string[];
  $: campussCounter = 0;

  onMount(() => {
    const ctx = chartCanvas.getContext("2d");

    chartjs.register(ChartDataLabels);

    new chartjs(ctx!, {
      type: "pie",
      options: {
        plugins: {
          datalabels: {
            formatter(value, context) {
              const data = context.dataset.data;
              const sum = data.reduce(
                (acc, d) => (acc as number) + (d as number),
                0
              )! as number;
              const percentage = Math.floor((value / sum) * 100);
              const currentCampus = Object.keys(campus).find(
                (c) => campus[c as keyof Node] == value
              );
              return `${currentCampus} ${percentage}%`;
            },
            labels: {
              title: {
                font: {
                  weight: "bold"
                }
              },
              value: {
                color: "white"
              }
            }
          }
        }
      },
      data: {
        labels: Object.keys(campus),
        datasets: [
          {
            label: "campus",
            backgroundColor: Object.keys(campus).map(
              () =>
                `rgb(${Math.random() * 255}, ${Math.random() * 255}, ${Math.random() * 255})`
            ),
            borderColor: "rgb(255, 99, 132)",
            data: Object.values(campus)
          }
        ]
      }
    });

    const text = `With different campuss adding up to`;

    text.split(" ").forEach((t, index) => {
      setTimeout(() => {
        campussText = [...campussText, t];

        if (index === text.split(" ").length - 1) {
          for (let i = 0; i <= Object.keys(campus).length; i++) {
            setTimeout(() => {
              campussCounter = i;
            }, 200);
          }
        }
      }, index * 200);
    });
  });
</script>

<div>
  <div class="h-screen">
    <div class="flex flex-wrap items-center justify-center">
      {#each campussText as text, index (index)}
        <p transition:fade class="text-4xl font-bold text-gray-800">
          {text}<span>&nbsp;</span>
        </p>
      {/each}
    </div>

    {#if campussCounter > 0}
      <p
        transition:fly={{ y: 700 }}
        class="text-green-500 text-center text-[7rem] lg:text-[24rem]"
      >
        {campussCounter}
      </p>
    {/if}
  </div>
  <div class="flex flex-col justify-center items-center">
    <div class="lg:w-[50rem] lg:h-[50rem] w-[28rem] h-[28rem]">
      <canvas class="" bind:this={chartCanvas}></canvas>
    </div>
  </div>
</div>
