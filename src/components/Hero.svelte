<script lang="ts">
  import { fly, fade } from "svelte/transition";
  import { onMount } from "svelte";
  export let featuredYear: string;
  export let totalApplicants: number;

  $: heroText = [] as string[];
  $: applicantsCounter = 0;

  onMount(() => {
    const text = `For the ${featuredYear} academic year, the total number of successful applicants were`;

    text.split(" ").forEach((t, index) => {
      setTimeout(() => {
        heroText = [...heroText, t];

        if (index === text.split(" ").length - 1) {
          for (let i = 0; i <= totalApplicants; i++) {
            setTimeout(() => {
              applicantsCounter = i;
            }, 200);
          }
        }
      }, index * 200);
    });
  });
</script>

<div class="h-screen">
  <div class="flex flex-wrap items-center justify-center">
    {#each heroText as text, index (index)}
      <p transition:fade class="text-4xl font-bold text-gray-800">
        {text}<span>&nbsp;</span>
      </p>
    {/each}
  </div>

  {#if applicantsCounter > 0}
    <p transition:fly={{y: 700}} class="text-green-500 text-center text-[7rem] lg:text-[24rem]">{applicantsCounter}</p>
  {/if}
</div>
