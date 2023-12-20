using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfileService.Data;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Repositories;

public class SecurityQuestionRepository(ProfileDataContext context) : ISecurityQuestionRepository
{
    private readonly ProfileDataContext _context = context;

    public async Task<SecurityQuestion> CreateSecurityQuestionAsync(
        SecurityQuestionDTO securityQuestionDTO
    )
    {
        SecurityQuestion newSecurityQuestion =
            new(
                Guid.NewGuid(),
                securityQuestionDTO.UserUuid,
                securityQuestionDTO.Question,
                securityQuestionDTO.Answer
            );

        EntityEntry<SecurityQuestion> securityQuestionEntry =
            await _context.SecurityQuestions.AddAsync(newSecurityQuestion);
        await _context.SaveChangesAsync();
        return securityQuestionEntry.Entity;
    }

    public async Task<List<SecurityQuestion>> GetSecurityQuestionsByIdAsync(Guid userUuid)
    {
        List<SecurityQuestion> queryedSecurityQuestions = await _context
            .SecurityQuestions.Where(sq => sq.UserUuid == userUuid)
            .ToListAsync();
        return queryedSecurityQuestions;
    }

    public async Task<List<SecurityQuestion>> UpdateSecurityQuestionsAsync(
        List<SecurityQuestion> securityQuestions
    )
    {
        // List<SecurityQuestion> existingSecurityQuestions = await _context.SecurityQuestions.Where(sq => securityQuestions.Select(dto => dto.Uuid).Contains(sq.Uuid)).ToListAsync();
        List<SecurityQuestion> queryedSecurityQuestions = await _context
            .SecurityQuestions.Where(sq => sq.UserUuid == securityQuestions[0].UserUuid)
            .ToListAsync();

        foreach (SecurityQuestion updatedSecurityQuestion in queryedSecurityQuestions)
        {
            SecurityQuestion securityQuestion = securityQuestions.Single(
                sq => sq.Uuid == updatedSecurityQuestion.Uuid
            );

            updatedSecurityQuestion.Question = securityQuestion.Question;
            updatedSecurityQuestion.Answer = securityQuestion.Answer;
        }

        await _context.SaveChangesAsync();
        return queryedSecurityQuestions;
    }

    public async Task DeleteSecurityQuestionAsync(Guid uuid)
    {
        SecurityQuestion queryedSecurityQuestion = await _context.SecurityQuestions.SingleAsync(
            sq => sq.Uuid == uuid
        );
        _context.SecurityQuestions.Remove(queryedSecurityQuestion);
        await _context.SaveChangesAsync();
    }
}
