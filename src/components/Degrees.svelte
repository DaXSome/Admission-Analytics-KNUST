<script lang="ts">
  import { onMount } from "svelte";
  import { fly, fade } from "svelte/transition";
  import ChartDataLabels from "chartjs-plugin-datalabels";
  import chartjs from "chart.js/auto";

  interface Node {
    degree: string;
    count: number;
  }

  export let degrees: Node[];

  let chartCanvas: HTMLCanvasElement;

  $: degreesText = [] as string[];
  $: degreesCounter = 0;

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
              const degree = degrees.find(
                ({ count }) => count === value
              )?.degree;
              return percentage < 5 ? "" : `${degree} ${percentage}%`;
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
        labels: degrees.map(({ degree }) => degree),
        datasets: [
          {
            label: "Degrees",
            backgroundColor: degrees.map(
              () =>
                `rgb(${Math.random() * 255}, ${Math.random() * 255}, ${Math.random() * 255})`
            ),
            borderColor: "rgb(255, 99, 132)",
            data: degrees.map(({ count }) => count)
          }
        ]
      }
    });

    const text = `With different degrees adding up to`;

    text.split(" ").forEach((t, index) => {
      setTimeout(() => {
        degreesText = [...degreesText, t];

        if (index === text.split(" ").length - 1) {
          for (let i = 0; i <= degrees.length; i++) {
            setTimeout(() => {
              degreesCounter = i;
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
      {#each degreesText as text, index (index)}
        <p transition:fade class="text-4xl font-bold text-gray-800">
          {text}<span>&nbsp;</span>
        </p>
      {/each}
    </div>

    {#if degreesCounter > 0}
      <p
        transition:fly={{ y: 700 }}
        class="text-green-500 text-center text-[7rem] lg:text-[24rem]"
      >
        {degreesCounter}
      </p>
    {/if}
  </div>
  <div class="flex flex-col justify-center items-center">
    <div class="lg:w-[50rem] lg:h-[50rem] w-[28rem] h-[28rem]">
      <canvas class="" bind:this={chartCanvas}></canvas>
    </div>
  </div>
</div>
