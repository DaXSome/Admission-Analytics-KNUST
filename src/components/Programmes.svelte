<script lang="ts">
  import { onMount } from "svelte";
  import { fly, fade } from "svelte/transition";
  import ChartDataLabels from "chartjs-plugin-datalabels";
  import chartjs from "chart.js/auto";

  interface Node {
    programme: string;
    count: number;
  }

  export let programmes: Node[];

  let chartCanvas: HTMLCanvasElement;
  let sortedProgrammes: Node[];

  $: programmesText = [] as string[];
  $: programmesCounter = 0;

  onMount(() => {
    sortedProgrammes = programmes.sort((a, b) => b.count - a.count);

    const ctx = chartCanvas.getContext("2d");

    chartjs.register(ChartDataLabels);

    new chartjs(ctx!, {
      type: "bar",
      options: {
        plugins: {
          datalabels: {
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
        labels: sortedProgrammes.slice(0, 10).map(({ programme }) => programme),
        datasets: [
          {
            label: "Top 10 Programmes",
            backgroundColor: programmes.map(
              () =>
                `rgb(${Math.random() * 255}, ${Math.random() * 255}, ${Math.random() * 255})`
            ),
            borderColor: "rgb(255, 99, 132)",
            data: sortedProgrammes.slice(0, 10).map(({ count }) => count)
          }
        ]
      }
    });

    const text = `With different programmes adding up to`;

    text.split(" ").forEach((t, index) => {
      setTimeout(() => {
        programmesText = [...programmesText, t];

        if (index === text.split(" ").length - 1) {
          for (let i = 0; i <= programmes.length; i++) {
            setTimeout(() => {
              programmesCounter = i;
            }, 200);
          }
        }
      }, index * 200);
    });
  });
</script>

<div>
  <div class="h-screen flex flex-col items-center justify-center">
    <div class="flex flex-wrap items-center justify-center">
      {#each programmesText as text, index (index)}
        <p transition:fade class="text-4xl font-bold text-gray-800">
          {text}<span>&nbsp;</span>
        </p>
      {/each}
    </div>

    {#if programmesCounter > 0}
      <p
        transition:fly={{ y: 700 }}
        class="text-green-500 text-center text-[7rem] lg:text-[24rem]"
      >
        {programmesCounter}
      </p>
    {/if}
  </div>
  <div class="flex flex-col justify-center items-center">
    <div class="lg:w-[50rem] lg:h-[50rem] w-[28rem] h-[28rem]">
      <canvas class="" bind:this={chartCanvas}></canvas>
    </div>
  </div>
</div>
