using ApiFinalProject.DTO.Chapter;
using ApiFinalProject.Services.Chapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChaptersController(IChapterService chapterService) : ControllerBase
{
    private readonly IChapterService _chapterService = chapterService;

    [HttpGet("{id}")]
    public async Task<ActionResult<ChapterResponseDTO>> GetChapter(int id)
    {
        var chapter = await _chapterService.GetChapterByIdAsync(id);
        if (chapter == null)
            return NotFound();

        return Ok(chapter);
    }

    [HttpPost]
    public async Task<ActionResult<ChapterResponseDTO>> CreateChapter([FromBody] ChapterRequestDTO chapterRequest)
    {
        var chapter = await _chapterService.CreateChapterAsync(chapterRequest);
        return CreatedAtAction(nameof(GetChapter), new { id = chapter.Id }, chapter);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateChapter(int id, [FromBody] ChapterRequestDTO chapterRequest)
    {
        var updated = await _chapterService.UpdateChapterAsync(id, chapterRequest);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteChapter(int id)
    {
        var deleted = await _chapterService.DeleteChapterAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
