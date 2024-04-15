<script lang="ts">
  import featured from "./data/2023-2024.json";
  import { onMount } from "svelte";
  import Hero from "./components/Hero.svelte";
  const featuredYear = "2023 - 2024";

  const applicants = featured.applicants;

  const allDegrees = Array.from(
    new Set(applicants.map(({ programme }) => programme.split(" ")[0]))
  );

  const degreeCounter = allDegrees.map((degree) => ({
    degree,
    count: applicants.filter(
      ({ programme }) => programme.split(" ")[0] === degree
    ).length
  }));

  const allProgrammes = Array.from(
    new Set(applicants.map(({ programme }) => programme))
  );

  const programmesCounter = allProgrammes.map((p) => ({
    programme: p,
    count: applicants.filter(({ programme }) => programme === p).length
  }));

  const isObuasiCampusCount = applicants.filter(({ programme }) =>
    programme.toLowerCase().includes("obuasi")
  ).length;

  $: canStart = false;

  onMount(() => {
    canStart = true;
  });
</script>

<main class="">
  {#if canStart}
    <Hero {featuredYear} totalApplicants={applicants.length} />
  {/if}
</main>

<style>
</style>
